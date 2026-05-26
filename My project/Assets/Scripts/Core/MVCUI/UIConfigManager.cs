using MySinleton;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Tool.MyAB;
using UnityEngine;
using XLua;

/// <summary>
/// 【单一职责】仅管理UI配置数据，不加载AB，不操作UI
/// </summary>
public class UIConfigManager : Singleton<UIConfigManager>
{
    public Dictionary<string, UIConfigItem> _configCache = new();
    const string abName = "configassets";
    const string ResName = "UISOConfigs";
    public bool IsConfigLoaded { get; private set; }

    /// <summary>
    /// 【GameEntry 调用】初始化：加载ConfigAB里的SO总配置
    /// </summary>
    public async Task InitConfig()
    {
        var tcs = new TaskCompletionSource<bool>();
        ABManager.Instance.LoadResAsync(abName, ResName, typeof(UISOConfigs), (so) =>
        {
            // 解析SO，存入字典缓存
            _configCache.Clear();
            UISOConfigs UIConfigItemSO = so as UISOConfigs;
            foreach (var item in UIConfigItemSO.allUIConfigs)
            {
                _configCache[item.uiName] = item;
            }
            IsConfigLoaded = true;
            tcs.SetResult(true);
        });
        await tcs.Task;
    }

    /// <summary>
    /// 【UIManager 调用】同步获取配置
    /// </summary>
    public UIConfigItem GetUIConfig(string uiName)
    {
        _configCache.TryGetValue(uiName, out var config);
        if (config != null)
        {
            Debug.Log($"[UIConfigManager] 成功加载{uiName}的config资源");
            return config;
        }
        else
        {
            Debug.LogError($"[UIConfigManager] {uiName}的config资源加载出错");
            return null;
        }
    }

    #region 清理缓存相关
    /// <summary>
    /// 清理（切换账号/退出游戏才调用）
    /// </summary>
    [LuaCallCSharp]
    public async Task ClearCache()
    {
        _configCache.Clear();
        IsConfigLoaded = false;
    }
    #endregion
}