using System;
using System.Collections;
using System.Collections.Generic;
using Tool.MyAB;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

[XLua.LuaCallCSharp]
public class PlayerController : CharacterControllerBase // 专注玩家输入与状态驱动
{
    #region 参数属性
    public Vector2 _inputDirection;
    public Vector3 pos;
    public Vector2 _lookInput;
    public float _zoomInput;
    public Transform LockTarget;
    public bool HasMoveInput => _inputDirection.magnitude > 0.1f;
    public bool HasRunInput => inputActions.GamePlay.Run.IsPressed();
    public bool HasCrouchInput => inputActions.GamePlay.Crouch.IsPressed();
    public bool HasLockTargetInput => inputActions.GamePlay.LockTarget.IsPressed();
    public bool HasLookInput => _lookInput.magnitude > 0.01f;
    public Vector2 LookInput => _lookInput;
    public float ZoomInput => _zoomInput;

    #endregion
    protected void Awake()
    {
        //玩家数据初始化
        playerHotLogic = new PlayerHotLogic();
        playerHotLogic.Init(this);
        Init();
        #region 事件注册
        //EventCenter.Instance.Register(
        //"",
        //new Action(),
        //owner: this,
        //once: false
        //);
        #endregion
    }
    //处理逻辑
    protected override void OnUpdate()
    {
        base.OnUpdate();

        //驱动状态机
        playerMovementStateMachine.OnUpdate();//状态机一变，通过StateMechineBase中的currentState?.OnUpdate();执行切换的state的变更逻辑

        playerHotLogic.Update(Time.deltaTime);

        EventCenter.Instance.Trigger("PlayerLuaUpdate");//CameraInput.lua
    }
    //处理物理逻辑
    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        UpdateInputCache();

        //驱动状态机
        playerMovementStateMachine.OnFixedUpdate();

        playerMovementStateMachine.UpdateTimer(Time.fixedDeltaTime);
    }
    #region 事件注册相关
    //private void OnLuaReady()
    //{

    //}

    #endregion
    private void OnDisable()
    {
        EventCenter.Instance.UnRegister(owner: this);
    }

    #region 封装检测输入
    void UpdateInputCache()
    {
        _inputDirection = inputActions.GamePlay.Move.ReadValue<Vector2>();
        _lookInput = inputActions.GamePlay.Look.ReadValue<Vector2>();
        _zoomInput = inputActions.GamePlay.ScrollZoom.ReadValue<float>();
    }
    #endregion

    public void MoveCharacter(Vector3 motion) { characterController?.Move(motion); }
    public bool IsGrounded => isGrounded;
    public Vector3 GetCameraForward() => CameraManager.Instance?.MainCamera?.transform.forward ?? Vector3.forward;
    public Vector3 GetCameraRight() => CameraManager.Instance?.MainCamera?.transform.right ?? Vector3.right;

}