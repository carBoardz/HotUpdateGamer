using MySinleton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tool.MyAB;
using UnityEngine;
using UnityEngine.SceneManagement;
using XLua;

[LuaCallCSharp]
public class LoadSceneMgr : SingletonMono<LoadSceneMgr>
{
    public SceneConfigSO currentScene;
    public int currentIndex = -1;
    SceneListSO _SceneListConfig;

    const string SOABName = "configassets";
    const string ResName = "AllScenesSO";
    const string SceneABName = "Scene";

    protected override void Awake()
    {
        base.Awake();
        if (!IsValidSingleton) return;
        
        EventCenter.Instance.Register(
        "LuaEnv_Ready",
        new Action(RegisteToLua),
        owner: this,
        once: false
        );
    }
    /// <summary>
    /// 加载关卡列表
    /// </summary>
    public async Task InitLevelList()
    {
        var soLoadedTcs = new TaskCompletionSource<bool>();
        ABManager.Instance.LoadResAsync(
            SOABName,
            ResName,
            typeof(SceneListSO),
            (obj) =>
            {
                if (obj != null)
                {
                    _SceneListConfig = obj as SceneListSO;
                    soLoadedTcs.SetResult(true);
                    Debug.Log("关卡列表加载完成，总关卡数：" + _SceneListConfig.levelList.Count);
                }
                else
                {
                    soLoadedTcs.SetResult(false);
                    Debug.LogError("关卡列表加载失败，obj 为空");
                }
            }
        );
        //var sceneAbLoadedTcs = new TaskCompletionSource<bool>();
        //ABManager.Instance.LoadABOnlyAsync(
        //    SceneABName,
        //    (ok) =>
        //    {
        //        if (ok)
        //        {
        //            sceneAbLoadedTcs.SetResult(true);
        //            Debug.Log("AB包scene已成功加载：" + _SceneListConfig.levelList.Count);
        //        }
        //        else
        //        {
        //            sceneAbLoadedTcs.SetResult(false);
        //            Debug.LogError("AB包scene成功失败");
        //        }
        //    }
        //);
        await Task.WhenAll(soLoadedTcs.Task);//, sceneAbLoadedTcs.Task
    }
    /// <summary>
    /// 通过索引来加载指定的场景关卡
    /// </summary>
    /// <param name="index"></param>
    [LuaCallCSharp]
    public async Task LoadSceneByIndex(int index)
    {
        if (_SceneListConfig == null || index < 0 || index >= _SceneListConfig.levelList.Count)
        {
            Debug.LogError($"关卡索引{index} 无效！");
            return;
        }
        try
        {
            UIManager.Instance.ClearAll();

            //显示过渡
            //await LoadingManager.Instance.ShowAsync("Warping to next sector...");//要改

            currentIndex = index;
            currentScene = _SceneListConfig.levelList[currentIndex];

            //Debug.Log($"[LoadSceneMgr] 准备加载场景: index={index}, scenePath={currentScene.scenePath}, assetBundleName={currentScene.assetBundleName}");

            var tcs = new TaskCompletionSource<bool>();
            ABManager.Instance.LoadABOnlyAsync(
            currentScene.assetBundleName,
                (ok) =>
                {
                    if (!ok) Debug.LogError($"加载场景 AB 包失败：{currentScene.assetBundleName}");
                    tcs.SetResult(ok);
                }
            );
            await tcs.Task;

            if (ABManager.Instance._abCache.TryGetValue(currentScene.assetBundleName, out var cacheData))
            {
                string[] assets = cacheData.ab.GetAllAssetNames();
                foreach (var asset in assets)
                    Debug.Log(" - " + asset);
            }
            else
            {
                Debug.LogError($"[LoadSceneMgr] AB包 {currentScene.assetBundleName} 不在缓存中！");
            }

            // 用包内路径加载场景
            var asyncOp = SceneManager.LoadSceneAsync(currentScene.scenePath, LoadSceneMode.Single);
            asyncOp.allowSceneActivation = true; // 或根据需要控制
            while (!asyncOp.isDone)
                await Task.Yield();
        }
        catch (Exception ex)
        {
            Debug.LogError($"场景index{index}加载错误，原因{ex}");
        }
    }
    /// <summary>
    /// 暴露给lua调用
    /// </summary>
    /// <param name="onComplete"></param>
    [LuaCallCSharp]
    public async void NextLevelAsync(Action onComplete)
    {
        await NextLevel();
        onComplete?.Invoke();
    }
    /// <summary>
    /// 加载下一关（通关调用，给Lua用）
    /// </summary>
    public async Task NextLevel()
    {
        await LoadSceneByIndex(currentIndex + 1);
    }
    #region 事件注册相关
    void RegisteToLua()
    {
        LuaMgr.Instance.Global.Set("LoadSceneMgr", LoadSceneMgr.Instance);
    }
    #endregion
    #region 清理缓存相关
    public async Task ClearCache()
    {
        _SceneListConfig = null;
        currentIndex = -1;
    }
    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
        ClearCache();
    }
    #endregion
}
