using MySinleton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using TMPro;
using Tool.MyAB;
using UnityEngine;
using UnityEngine.UI;
using XLua;

public class LoadingManager : SingletonMono<LoadingManager>
{
    const string LoadingUIConfigName = "UILoading";
    BaseView _loadingView;
    LuaTable _loadingLuaController;

    TaskCompletionSource<bool> _showTcs; // 用于等待显示动画完成
    TaskCompletionSource<bool> _hideTcs; // 用于等待隐藏动画完成

    public async Task ShowAsync(string loadingText = "Check for resource updates...")
    {
        if (_loadingView == null)
        {
            _loadingView = await UIManager.Instance.OpenUIAsync(LoadingUIConfigName, UILayer.Normal);
            if (_loadingView == null) return;
            _loadingLuaController = _loadingView._luaController;
            ApplyFontToLoadingText();
        }
        else
        {
            _loadingView.gameObject.SetActive(true);
        }

        _showTcs = new();
        _loadingLuaController?.Get<LuaFunction>("OnShow")?.Call(_loadingLuaController, loadingText);
        await _showTcs.Task;
    }

    public void UpdateProgress(long downloadedBytes, long totalBytes, float DownLoadProgress, string msg = null)
    {
        _loadingLuaController?.Get<LuaFunction>("UpdateProgress")?.Call(_loadingLuaController, downloadedBytes, totalBytes, DownLoadProgress, msg);
    }
    public async Task HideAsync(string loadingText = "Initialization complete...")
    {
        _hideTcs = new();
        _loadingLuaController?.Get<LuaFunction>("OnHide")?.Call(_loadingLuaController, loadingText);
        await _hideTcs.Task;

        // 真正的关闭交给 UIManager 回收/隐藏
        UIManager.Instance.CloseUI(LoadingUIConfigName);
        _loadingView = null;
        _loadingLuaController = null;
    }
    /// <summary>
    /// 供 Lua 动画事件调用
    /// </summary>
    public void OnShowAnimFinished()
    {
        Debug.Log("[LoadingManager] OnShowAnimFinished invoked");
        _showTcs?.TrySetResult(true);
    }
    public void OnHideAnimFinished()
    {
        _hideTcs?.TrySetResult(true);
    }
    /// <summary>
    /// 重置 LoadingManager 状态（游戏重启时调用）
    /// </summary>
    public async Task Reset()
    {
        _loadingView = null;
        _loadingLuaController = null;
        _showTcs = null;
        _hideTcs = null;
    }
    /// <summary>
    /// 确保将FontText(文本资源)挂载到text资源上
    /// </summary>
    public async Task ApplyFontToLoadingText()
    {
        try
        {
            var loadingTextComp = _loadingView.GetWidget("LoadingText_TextMeshProUGUI") as TextMeshProUGUI;
            if (loadingTextComp != null && loadingTextComp.font == null)
            {
                // 正确路径：从Resources内部开始，无扩展名
                TMP_FontAsset font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
                if (font != null)
                {
                    loadingTextComp.font = font;
                    Debug.Log("[LoadingManager] 字体已从 Resources 加载并赋值");
                }
                else
                {
                    // 使用内置备用字体
                    font = Resources.GetBuiltinResource<TMP_FontAsset>("LiberationSans SDF");
                    if (font != null) loadingTextComp.font = font;
                    Debug.LogWarning("[LoadingManager] 未能在 Resources 中找到字体，已使用备用字体");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"字体资源加载错误 {ex}");
        }
    }
}
//[其他业务]  →  LoadingService(静态工具类)
//                    ↓ 调用
//              UIManager.OpenUIAsync("LoadingPanel")  ← 复用框架
//                    ↓ 返回
//              BaseView (内部持有 LuaController)
//                    ↓ 调用 Lua 方法
//              LoadingPanel 的 Lua Controller (OnShow, UpdateProgress, OnHide...)