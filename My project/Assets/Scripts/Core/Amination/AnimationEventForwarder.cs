using UnityEngine;


public class AnimationEventForwarder : StateMachineBehaviour
{
    public enum PlayerAnimState
    {
        Idle,
        Walk,
        Run,
        Jump,
        Fall,
        Attack,
        Injured,
        Dead
    }
    [SerializeField] public PlayerAnimState onEnterAnimationState;

    public string eventName; // 在 Animator 窗口中填写的事件名，如 "OnAttackHit"

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 通过 EventCenter 触发事件，Lua 侧监听
        EventCenter.Instance.Trigger("AnimEvent_Enter", eventName, animator);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EventCenter.Instance.Trigger("AnimEvent_Exit", eventName, animator);
    }
}