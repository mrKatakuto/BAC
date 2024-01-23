using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandUpState : PlayerBaseState
{
    private readonly int StandUpAnimationHash = Animator.StringToHash("StandUp");
    private const float CrossFadeDuration = 0.1f;
    private bool isAnimationDone = false;

    public PlayerStandUpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(StandUpAnimationHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "StandUp");

            if (!isAnimationDone && normalizedTime >= 1)
            {
                isAnimationDone = true;
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
    }

    public override void Exit() 
    { 
        //stateMachine.Animator.CrossFadeInFixedTime(StandUpAnimationHash, CrossFadeDuration);
    }

    public void OnStandUpAnimationComplete()
    {
    stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

}

