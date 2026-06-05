using System;
using System.Collections.Generic;
using XLua;

[LuaCallCSharp]
public static class XLuaGenConfig
{
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>()
    {
        typeof(UnityEngine.Animator),
        typeof(UnityEngine.Rigidbody),
        typeof(UnityEngine.Transform),
        // 其他系统类...
    };
}