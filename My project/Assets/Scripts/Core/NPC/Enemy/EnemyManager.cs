using System.Collections.Generic;
using UnityEngine;
using MySinleton;

[XLua.LuaCallCSharp]
public class EnemyManager : SingletonMono<EnemyManager>
{
    // 所有存活敌人的 Transform（公开给 Lua）
    public List<Transform> enemyTransforms = new List<Transform>();

    /// <summary>
    /// 敌人生成时调用，向管理器注册
    /// </summary>
    public void RegisterEnemy(Transform enemyTransform)
    {
        if (!enemyTransforms.Contains(enemyTransform))
        {
            enemyTransforms.Add(enemyTransform);
        }
    }

    /// <summary>
    /// 敌人死亡或销毁时调用，从管理器移除
    /// </summary>
    public void UnregisterEnemy(Transform enemyTransform)
    {
        enemyTransforms.Remove(enemyTransform);
    }

    /// <summary>
    /// 获取所有存活敌人的 Transform（只读，避免 Lua 直接修改列表）
    /// </summary>
    public List<Transform> GetEnemyList()
    {
        return enemyTransforms;
    }
}