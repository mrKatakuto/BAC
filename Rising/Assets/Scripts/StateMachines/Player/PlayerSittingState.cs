using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSittingState : PlayerBaseState
{
    private readonly int SittingAnimationHash = Animator.StringToHash("Sitting");
    private const float CrossFadeDuration = 0.1f;

    public PlayerSittingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
  
    }

    public override void Enter()
    {

        stateMachine.Animator.CrossFadeInFixedTime(SittingAnimationHash, CrossFadeDuration);
        stateMachine.InputReader.IsSitting = true; 

    }

    public override void Tick(float deltaTime)
    {
 
        if (!stateMachine.InputReader.IsSitting)
        {

            stateMachine.SwitchState(new PlayerStandUpState(stateMachine));
        }
    }

    public override void Exit()
    {

    }
}
