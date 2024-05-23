using UnityEngine;

public class RobotFollowState : RobotBaseState
{
    private float stopDistance = 2.0f;

    public RobotFollowState(RobotStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        stateMachine.Animator.SetFloat("Speed", 0);
    }

    public override void Tick(float deltaTime)
    {
        if (!IsPlayerInRange())
        {
            stateMachine.SwitchState(new RobotIdleState(stateMachine));
            return;
        }

        float speed = UpdatePositionAndGetSpeed(deltaTime);
        stateMachine.Animator.SetFloat("Speed", speed);
    }

    private float UpdatePositionAndGetSpeed(float deltaTime)
    {
        Vector3 direction = (stateMachine.PlayerTransform.position - stateMachine.transform.position);
        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            direction.Normalize();
            float moveSpeed = 3.0f;  
            stateMachine.transform.position += direction * moveSpeed * deltaTime;
            stateMachine.transform.rotation = Quaternion.LookRotation(direction);

            return moveSpeed;  
        }
        return 0;  
    }

    public override void Exit()
    {
        stateMachine.Animator.SetFloat("Speed", 0);  
    }
}
