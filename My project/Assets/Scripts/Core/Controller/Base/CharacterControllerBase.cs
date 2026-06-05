using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XLua;

public class CharacterControllerBase : ControllerBase
{
    public PlayerHotLogic playerHotLogic { get; protected set; }
    public InputController inputActions { get; protected set; }
    public PlayerMovementStateMachine playerMovementStateMachine { get; protected set; }
    public PlayerAnimationController playerAnimationController { get; protected set; }
    public LuaPlayerState luaPlayerState { get; protected set; }
    public Animator animator;
    protected CharacterController characterController;
    protected Vector3 verticalVelocity;
    protected bool isGrounded;
    public float gravityMultiplier = 2.0f;
    protected override void Init()
    {
        base.Init();
        inputActions = new InputController();
        inputActions.Enable();

        playerMovementStateMachine = new PlayerMovementStateMachine(this as PlayerController);

        animator = GetComponent<Animator>();
        if (animator != null)
            animator.applyRootMotion = true;
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerAnimationController.Init(this as PlayerController, animator);

        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            characterController = gameObject.AddComponent<CharacterController>();
            // 根据你的角色模型调整这些参数
            characterController.center = new Vector3(0, 0.84f, 0);
            characterController.radius = 0.17f;
            characterController.height = 1.68f;
        }
    }
    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        ApplyGravity();
    }
    protected void ApplyGravity()
    {
        if (characterController == null) return;

        isGrounded = characterController.isGrounded;
        if (isGrounded && verticalVelocity.y < 0)
        {
            // 保持贴地
            verticalVelocity.y = -1f;
        }

        // 施加重力加速度
        verticalVelocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

        // 应用垂直移动
        characterController.Move(verticalVelocity * Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        if (animator == null) return;
        Vector3 motion = animator.deltaPosition;
        motion.y += verticalVelocity.y * Time.fixedDeltaTime;

        // 通过 CharacterController 移动，自动处理碰撞
        if (characterController != null && characterController.enabled)
        {
            characterController.Move(motion);
        }
        else
        {
            transform.position += motion;
        }
    }
}
