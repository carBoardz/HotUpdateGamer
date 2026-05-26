using UnityEngine;

[CreateAssetMenu(menuName = "创建SO/SceneLoadEventS0")]
public class SceneConfigSO: ScriptableObject
{
    [Header("场景所在的AB包名（如 configassets）")]
    public string assetBundleName;

    [Header("场景在AB包内的完整路径（如 assets/scenes/loadingmenu.unity）")]
    public string scenePath;    // 原来的 SceneName 改为 scenePath

    [Header("场景类型")]
    public SceneType type;
}
public enum SceneType
{
    Location, Menu
}