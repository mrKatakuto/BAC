using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    private RestartGame restartGame;

    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        restartGame = GameObject.FindObjectOfType<RestartGame>();
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.Weapon.gameObject.SetActive(false);

        if (restartGame != null)
            restartGame.ShowDeathPanel();
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {
        if (restartGame != null)
            restartGame.HideDeathPanel();
    }
}
