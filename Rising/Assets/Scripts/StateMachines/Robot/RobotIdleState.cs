using UnityEngine;

public class RobotIdleState : RobotBaseState
{
    public RobotIdleState(RobotStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {        
        stateMachine.Animator.SetFloat("Speed", 0);
    }

    public override void Tick(float deltaTime)
    {
        if (IsPlayerInRange())
        {
            stateMachine.SwitchState(new RobotFollowState(stateMachine));
        }
    }

    public override void Exit()
    {
    }
}
