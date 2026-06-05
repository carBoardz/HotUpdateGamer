using MySinleton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tool.MyAB;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static UnityEngine.GridBrushBase;

/// <summary>
/// 游戏唯一总入口
/// 职责：初始化管理器 → 预加载核心资源 → 启动Lua → 触发全局事件
/// </summary>
public class GameEntry : SingletonMono<GameEntry>
{
    // 核心AB包常量（统一管理，方便修改）
    private const string LuaBundleName = "luaassets";
    private const string ConfigName = "configassets";
    private const string PlayerName = "player";

    public static InitPhase CurrentPhase { get; private set; } = InitPhase.None;
    public static bool IsGlobalReady => CurrentPhase == InitPhase.AllReady;

    private static TaskCompletionSource<bool> _initTcs;

    protected override void Awake()
    {
        base.Awake();
        if (!IsValidSingleton) return;
        // 同步初始化所有管理器
        InitManagers();
    }

    private async void Start()
    {
        try
        {
            // 第一步：先初始化lua环境，为后续基础界面的lua脚本提供运行环境
            await PreloadAllCoreBundlesAsync();

            // 第二步：初始化 Lua 环境
            LuaMgr.Instance.Initialize();
            SetPhase(InitPhase.LuaEnv_Ready);

            // 第三步：执行 LuaMain，同时初始化
            LuaMgr.Instance.DoString("LuaMain");
            await InitConfig();
            SetPhase(InitPhase.ConfigLoaded);

            // 第四步：加载Loading场景
            await LoadSceneMgr.Instance.LoadSceneByIndex(0);
            await LoadingManager.Instance.ShowAsync();

            // 第五步：检测资源更新
            bool needRestart = await CheckAndDownloadUpdates();
            Debug.Log("needRestart:"+ needRestart);
            if (needRestart)
            {
                await RestartGameAsync();
            }

            // 第六步：触发全局事件（所有准备就绪）
            SetPhase(InitPhase.AllReady);
            Debug.Log("<color=green> 游戏启动流程全部完成！</color>");
        }
        catch (Exception e)
        {
            SetPhase(InitPhase.Failed);
            Debug.LogError($"游戏加载失败，原因:" + e);
        }
    }

    /// <summary>
    /// 统一预加载核心AB包（Lua/配置/动画）
    /// </summary>
    async Task PreloadAllCoreBundlesAsync()
    {
        var tcs = new TaskCompletionSource<bool>();
        int pendingCount = 3; // luaassets, configassets, player
        UnityAction<bool> onComplete = (ok) =>
        {
            if (!ok) Debug.LogError("核心 AB 包加载失败");
            pendingCount--;
            if (pendingCount == 0) tcs.SetResult(true);
        };

        ABManager.Instance.LoadABOnlyAsync(LuaBundleName, onComplete);
        ABManager.Instance.LoadABOnlyAsync(ConfigName, onComplete);
        ABManager.Instance.LoadABOnlyAsync(PlayerName, onComplete);
        await tcs.Task;
    }
    #region 初始化相关
    /// <summary>
    /// 同步初始化所有系统管理器
    /// </summary>
    void InitManagers()
    {
        new GameObject("EventCenter").AddComponent<EventCenter>();
        new GameObject("ABMgr").AddComponent<ABManager>();
        new GameObject("LuaMgr").AddComponent<LuaMgr>();
        new GameObject("LoadSceneMgr").AddComponent<LoadSceneMgr>();
        new GameObject("UIManager").AddComponent<UIManager>();
        new GameObject("PlayerManager").AddComponent<PlayerManager>();
        new GameObject("CameraManager").AddComponent<CameraManager>();
    }
    async Task InitConfig()
    {
        await UIConfigManager.Instance.InitConfig();
        await LoadSceneMgr.Instance.InitLevelList();
    }
    async Task SetPhase(InitPhase phase)
    {
        CurrentPhase = phase;
        Debug.Log($"[GameEntry] 初始化阶段 → {phase}");

        // 到达全就绪时，释放所有等待
        if (phase == InitPhase.AllReady)
        {
            _initTcs?.TrySetResult(true);
            await OnAllReady();
            EventCenter.Instance.Trigger(phase.ToString());
        }
        else if (phase == InitPhase.Failed)
        {
            _initTcs?.TrySetException(new Exception("初始化失败"));
        }
        else
        {
            EventCenter.Instance.Trigger(phase.ToString());
        }
    }
    #endregion
    private bool _isUpdating = false;
    /// <summary>
    /// 资源更新
    /// </summary>
    /// <returns></returns>
    async Task<bool> CheckAndDownloadUpdates()
    {
        if (_isUpdating)
        {
            Debug.LogWarning("更新已在执行");
            return false;
        }
        _isUpdating = true;
        Debug.Log("准备检测资源更新");
        bool success = await ABUpdateManager.Instance.DownLoadCompareFile();

        try
        {
            if (success)
            {
                bool ok = await ABUpdateManager.Instance.CheckUpdate((downloadedBytes, totalBytes, DownLoadProgress) =>
                {
                    LoadingManager.Instance.UpdateProgress(downloadedBytes, totalBytes, DownLoadProgress);
                });
                return ok;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"更新资源失败，原因：{ex}");
            _isUpdating = false;
            return false;
        }
    }
    public async Task RestartGameAsync()
    {
        // 1. 清理
        await ABManager.Instance.ClearAllABCache();
        await UIManager.Instance.ClearAll();
        await UIConfigManager.Instance.ClearCache();
        await LoadSceneMgr.Instance.ClearCache();
        await LoadingManager.Instance.Reset();
        LuaMgr.Instance.Dispose();
        
        // 2. 重新初始化（与 Start 保持一致）
        await PreloadAllCoreBundlesAsync();
        LuaMgr.Instance.Initialize();
        SetPhase(InitPhase.LuaEnv_Ready);
        LuaMgr.Instance.DoString("LuaMain");
        await InitConfig();
        await LoadSceneMgr.Instance.LoadSceneByIndex(0);
        await LoadingManager.Instance.ShowAsync();

        Debug.Log("<color=blue>游戏重启完成</color>");
    }
    async Task OnAllReady()
    {
        await UIManager.Instance.ClearAll();
        await UIManager.Instance.OpenUIAsync("MainPanel");
    }
    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }
}
public enum InitPhase
{
    None = 0,
    ManagersCreated,        // Awake 完成，所有单例创建完毕
    CoreBundlesLoaded,      // 核心 AB 包加载完成
    LuaEnv_Ready,            // Lua 环境就绪
    ConfigLoaded,           // 配置表加载完成
    SceneLoaded,            // 启动场景加载完成
    UpdateCheckDone,        // 资源更新检查完成
    AllReady,               // 全部就绪，可进入游戏
    Failed                  // 初始化失败
}