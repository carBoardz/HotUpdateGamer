using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tool.MyAB;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using XLua;
using static Unity.Burst.Intrinsics.X86.Avx;

public class BaseView : MonoBehaviour
{
    bool _widgetsCollected = false;
    Dictionary<string, Component> _widgets = new Dictionary<string, Component>();
    static Dictionary<string, LuaBindingCollector> _bindingSoCache = new();//有多个同名面板同时存在（比如多个 DamageText），也只需要加载一次 SO

    public LuaTable _luaController { get; private set; }
    public string UIName { get; set; }
    public void BindLuaController(LuaTable controller, object userData = null)
    {
        if(controller != null)
        _luaController = controller;//也就是o

        var onInit = _luaController.Get<LuaFunction>("OnInit");
        if (onInit != null)
            onInit?.Call(_luaController, this, userData);
        else
            Debug.LogError($"[BaseView] Lua Controller for {gameObject.name} has no OnInit method!");
    }
    #region 组件绑定相关
    /// <summary>
    /// 通过BindingSO获取对应的绑定组件
    /// </summary>
    /// <param name="bindingSO"></param>
    /// <summary>
    /// 【UIManager 调用】确保组件只收集一次
    /// </summary>
    public async Task PrepareWidgetsAsync(UIConfigItem UIConfig)
    {
        if (_widgetsCollected) return;
        _widgets.Clear();

        LuaBindingCollector luaBindingCollector = await LoadBindingSOAsync(UIConfig);

        foreach (var bind in luaBindingCollector.bindings)
        {
            CollectWidgetsFromSO(bind);
        }
        _widgetsCollected = true;
    }
    /// <summary>
    /// 异步加载每个绑定组件
    /// </summary>
    /// <param name="bindingSO"></param>
    /// <returns></returns>
    void CollectWidgetsFromSO(WidgetBinding bindingSO)
    {
        if (bindingSO == null) return;
        Component comp;
        if (string.IsNullOrEmpty(bindingSO.widgetPath))
        {
            // 根节点自身
            comp = GetComponent(bindingSO.componentType);
        }
        else
        {
            Transform child = transform.Find(bindingSO.widgetPath);
            comp = child.GetComponent(bindingSO.componentType);
        }
        if (comp != null)
        {
            _widgets[bindingSO.widgetName] = comp;
            RegisterWidgetEvents(bindingSO.widgetName,comp);
        }
        else
        {
            Debug.LogError($"[BaseView] 无法找到组件{bindingSO.widgetName}");
        }
    }
    /// <summary>
    /// 异步加载SOBindingConfig
    /// </summary>
    /// <param name="UIConfig">UI配置信息</param>
    /// <returns></returns>
    public async Task<LuaBindingCollector> LoadBindingSOAsync(UIConfigItem UIConfig)
    {
        if (_bindingSoCache.TryGetValue(UIConfig.bindingConfig, out var cachedSO))
            return cachedSO;

        var tcs = new TaskCompletionSource<LuaBindingCollector>();
        ABManager.Instance.LoadResAsync(UIConfig.abName, UIConfig.bindingConfig, typeof(LuaBindingCollector), (obj) =>
        {
            tcs.SetResult(obj as LuaBindingCollector);
        });

        LuaBindingCollector so = await tcs.Task;

        if (so != null)
            _bindingSoCache[UIConfig.bindingConfig] = so;
        else
            Debug.LogError($"[BaseView] 加载绑定SO失败：{UIConfig.bindingConfig}");

        return so;
    }
    #endregion
    public void CloseSelf()
    {
        UIManager.Instance.CloseUI(UIName);
    }
    /// <summary>
    /// 释放View视图
    /// </summary>
    public void DisposeView()
    {
        UnregisterWidgetEvents();
        _luaController.Get<LuaFunction>("DisposeView")?.Call(_luaController);
        _luaController?.Dispose();
        _luaController = null;
        _widgets.Clear();
        _widgetsCollected = false;
    }

    #region 为按钮等ui组件添加监听事件
    private void RegisterWidgetEvents(string widgetName, Component comp)
    {
        switch (comp)
        {
            case Button button:
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnButtonClick(widgetName));
                break;
            case Toggle toggle:
                toggle.onValueChanged.RemoveAllListeners();
                toggle.onValueChanged.AddListener((isOn) => OnToggleValueChanged(widgetName, isOn));
                break;
        }
    }

    private void UnregisterWidgetEvents()
    {
        foreach (var kvp in _widgets)
        {
            if (kvp.Value is Button button)
            {
                button.onClick.RemoveAllListeners();
            }
            if (kvp.Value is Toggle toggle)
            {
                toggle.onValueChanged.RemoveAllListeners();
            }
        }
    }
    #endregion
    
    #region 绑定lua中方法的函数相关
    public void OnButtonClick(string btnName)
    {
        if (_luaController == null) return;
        _luaController.Get<LuaFunction>("OnButtonClick")?.Call(_luaController, btnName);
    }
    public void OnToggleValueChanged(string togName, bool isOn)
    {
        if (_luaController == null) return;
        _luaController.Get<LuaFunction>("OnToggleValueChanged")?.Call(_luaController, togName, isOn);
    }
    /// <summary>
    /// 刷新View视图
    /// </summary>
    public void RefreshView()
    {
        _luaController.Get<LuaFunction>("RefreshView")?.Call(_luaController);
    }
    #endregion

    #region lua调用csharp相关函数
    [LuaCallCSharp]
    public Component GetWidget(string name)
    {
        _widgets.TryGetValue(name, out var comp);
        return comp;
    }
    #endregion
}
