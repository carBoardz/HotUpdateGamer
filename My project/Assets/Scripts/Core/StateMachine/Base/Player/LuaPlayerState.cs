using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using XLua;

public class LuaPlayerState : IState
{
    readonly LuaTable _luaState;

    Action _enter;
    Action _exit;
    Action _onUpdate;
    Action _onFixedUpdate;
    Action _onLateUpdate;

    public readonly PlayerMovementStateMachine stateMachine;
    public readonly PlayerController controller;
    public readonly PlayerAnimationController animContl;

    protected StateTimer _bufferTimer;
    protected bool isBuffering;

    public LuaPlayerState(PlayerMovementStateMachine sm, PlayerController c, LuaTable luaTable)
    {
        controller = c;
        stateMachine = sm;
        animContl = c.playerAnimationController;
        _luaState = luaTable;

        Initialize();
    }

    public void Initialize()
    {
        if (LuaMgr.Instance.Global == null) Debug.LogError("LuaMgr未被实例化");

        // 获取带 self 参数的委托版本
        var enterWithSelf = _luaState.Get<Action<LuaTable>>("Enter");
        var exitWithSelf = _luaState.Get<Action<LuaTable>>("Exit");
        var onUpdateWithSelf = _luaState.Get<Action<LuaTable>>("OnUpdate");
        var onFixedUpdateWithSelf = _luaState.Get<Action<LuaTable>>("OnFixedUpdate");
        var onLateUpdateWithSelf = _luaState.Get<Action<LuaTable>>("OnLateUpdate");

        // 包装成无参 Action，调用时自动传入 _luaState 作为 self
        _enter = () => enterWithSelf?.Invoke(_luaState);
        _exit = () => exitWithSelf?.Invoke(_luaState);
        _onUpdate = () => onUpdateWithSelf?.Invoke(_luaState);
        _onFixedUpdate = () => onFixedUpdateWithSelf?.Invoke(_luaState);
        _onLateUpdate = () => onLateUpdateWithSelf?.Invoke(_luaState);

        _luaState.Set("csharp", this);

        Debug.Log("LuaPlayerState 注入Lua成功");
    }
    public Animator GetAnimator()
    {
        if (animContl == null) return null;
        return animContl._animator;
    }
    #region 计时器方法
    /// <summary>
    /// 计时器结束回调函数
    /// </summary>
    public virtual void OnBufferComplete()
    {
        isBuffering = false;
        TimerPool.Recycle(_bufferTimer);
        _bufferTimer = null;
    }
    /// <summary>
    /// 从缓存池取出计时器同时初始化
    /// </summary>
    public virtual void StartBufferTime()
    {
        if (isBuffering) return;
        _bufferTimer = TimerPool.Get(0.11f);
        _bufferTimer.OnComplete = OnBufferComplete;
        _bufferTimer.Start();
        stateMachine.CurrentTimer = _bufferTimer;
        isBuffering = true;
    }
    /// <summary>
    /// 重置计时器
    /// </summary>
    public void RecycleTimer()
    {
        if (_bufferTimer != null)
        {
            _bufferTimer.Stop();
            _bufferTimer.Clear();
            TimerPool.Recycle(_bufferTimer);
            _bufferTimer = null;
        }
    }
    #endregion

    public void Enter()
    {
        _enter?.Invoke();
    }
    public void Exit()
    {
        _exit?.Invoke();
        TimerPool.Recycle(_bufferTimer);
        isBuffering = false;
    }
    public void OnUpdate()
    {
        _onUpdate?.Invoke();
    }

    public void OnFixedUpdate()
    {
        _onFixedUpdate?.Invoke();

        if (controller.HasMoveInput && !isBuffering)
        {
            StartBufferTime();
        }
        if (!controller.HasMoveInput)
        {
            isBuffering = false;
        }
    }
    public void OnLateUpdate()
    {
        _onLateUpdate?.Invoke();
    }
}
