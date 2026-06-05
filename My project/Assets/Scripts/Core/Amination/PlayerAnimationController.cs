using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tool.MyAB;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using XLua;

[LuaCallCSharp]
public class PlayerAnimationController : MonoBehaviour
{
    const string PLAYER_AB = "player";
    const string ANIM_CONTROLLER = "PlayerMainAnimator";

    public Animator _animator;
    
    public void Init(PlayerController playerController, Animator animator)
    {
        _animator = animator;

        // 从AB缓存加载动画控制器（只加载一次，常驻内存）
        
        RuntimeAnimatorController animCtrl =
            ABManager.Instance.LoadAssetSync<RuntimeAnimatorController>(PLAYER_AB, ANIM_CONTROLLER);

        if (animCtrl != null)
            _animator.runtimeAnimatorController = animCtrl;
    }
}
