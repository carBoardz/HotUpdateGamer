using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class AIMovementStateMachine : MovementStateMachineBase
{
    public readonly Dictionary<string, IState> _stateDict;

    public AIMovementStateMachine(AIController controller) : base(controller)
    {
        _stateDict = new();
    }
    public void SetInitialState(string stateName)
    {
        Initialize(stateName);
    }
    /// <summary>
    /// Lua 友好的初始化方法：通过状态名字符串设置初始状态
    /// </summary>
    public void Initialize(string stateName)
    {
        if (_stateDict.TryGetValue(stateName, out var state))
        {
            Initialize(state);  // 调用基类的 Initialize(IState)
        }
        else
        {
            Debug.LogError($"状态 '{stateName}' 未注册！");
        }
    }
    public void LuaRisterState(string StateName, LuaTable luaState)
    {
        Debug.Log("LuaRisterState 被调用");
        //var state = new LuaEnemyState(this, playerController, luaState);
        //if (!_stateDict.ContainsKey(StateName))
        //    _stateDict.Add(StateName, state);
    }
    public void SwitchState(string stateName)
    {
        if (!_stateDict.ContainsKey(stateName))
        {
            Debug.LogError($"状态不存在：{stateName}");
            return;
        }
        ChangeState(_stateDict[stateName]);
    }
    public override void OnUpdate()
    {
        currentState?.OnUpdate();
    }
    public override void OnFixedUpdate()
    {
        currentState?.OnFixedUpdate();
    }
}