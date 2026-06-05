using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SceneSOGenerater : EditorWindow
{
    static string directoryPath = "Assets/Resource/HotRes/Date/SO/Scene/SceneListConfig";

    static string obtainDirectoryPath = "Assets/Scenes";

    [MenuItem("Assets/SceneSO/生成Scene配置信息", false, 100)]
    [Tooltip("生成每个Scene的配置信息，配置文件生成在目录Assets/Resource/HotRes/Date/SO/Scene/SceneSO/")]
    public static void GenerateSceneConfig()
    {
        //确保Scene的路径存在
        if (!AssetDatabase.IsValidFolder(obtainDirectoryPath))
        {
            EditorUtility.DisplayDialog("错误", $"请先创建文件路径 {obtainDirectoryPath} 并存入场景资源", "喵~");
            return;
        }

        var sceneAssets = LoadAllAssets<SceneAsset>(obtainDirectoryPath);
        if (sceneAssets.Count == 0)
        {
            EditorUtility.DisplayDialog("提示", "在 Scenes 目录下没有找到任何场景文件", "喵~");
            return;
        }

        //确保生成SO的路径存在
        if (!AssetDatabase.IsValidFolder(directoryPath))
        {
            CheckAndCreateFolder();
            AssetDatabase.Refresh();
        }

        // 获取SceneListS0
        string sceneListDir = Path.GetDirectoryName(directoryPath);
        string sceneListPath = Path.Combine(sceneListDir, "AllScenesSO.asset");
        SceneListSO sceneListSO = AssetDatabase.LoadAssetAtPath<SceneListSO>(sceneListPath);
        bool isNew = sceneListSO == null;
        if (isNew)
            sceneListSO = ScriptableObject.CreateInstance<SceneListSO>();
        sceneListSO.levelList = new List<SceneConfigSO>();

        foreach (var sceneAsset in sceneAssets)
        {
            string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
            if (string.IsNullOrEmpty(scenePath)) continue;

            // 包内路径统一小写，去掉 "Assets/" 前缀
            string bundlePath = scenePath;
            string bundleName = AssetDatabase.GetImplicitAssetBundleName(scenePath);

            SceneConfigSO configSO = ScriptableObject.CreateInstance<SceneConfigSO>();
            configSO.assetBundleName = string.IsNullOrEmpty(bundleName) ? "default" : bundleName;
            configSO.scenePath = bundlePath;
            configSO.type = SceneType.Menu;

            //保存
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);
            string configAssetPath = Path.Combine(directoryPath, $"{sceneName}_Config.asset");
            AssetDatabase.CreateAsset(configSO, configAssetPath);

            sceneListSO.levelList.Add(configSO);
        }

        if (isNew)
            AssetDatabase.CreateAsset(sceneListSO, sceneListPath);

        EditorUtility.SetDirty(sceneListSO);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("成功", $"已生成 {sceneAssets.Count} 个场景配置", "喵~");
    }
    /// <summary>
    /// 检测文件是否存在并创建
    /// </summary>
    /// <param name="parentPath">父级文件路径</param>
    /// <param name="folderName">目标文件名</param>
    /// <returns></returns>
    public static void CreateFolderIfNotExist(string fullPath)
    {
        if (AssetDatabase.IsValidFolder(fullPath))
            return;

        string parent = Path.GetDirectoryName(fullPath);
        string folder = Path.GetFileName(fullPath);

        if (!AssetDatabase.IsValidFolder(parent))
        {
            CreateFolderIfNotExist(parent);
        }

        AssetDatabase.CreateFolder(parent, folder);
        AssetDatabase.Refresh();
    }
    static void CheckAndCreateFolder()
    {
        CreateFolderIfNotExist(directoryPath);
    }

    public static List<T> LoadAllAssets<T>(string path) where T : Object
    {
        List<T> assets = new();
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { path });
        foreach (var guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset != null) assets.Add(asset);
        }
        return assets;
    }
}
