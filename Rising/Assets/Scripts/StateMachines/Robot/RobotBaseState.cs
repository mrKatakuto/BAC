using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class RobotBaseState: State 
{
    protected RobotStateMachine stateMachine;

    public RobotBaseState(RobotStateMachine stateMachine) 
    {
        this.stateMachine = stateMachine;
    }

    
    protected bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector3.Distance(stateMachine.transform.position, stateMachine.PlayerTransform.position);
        return distanceToPlayer <= stateMachine.followDistance;
    }

}