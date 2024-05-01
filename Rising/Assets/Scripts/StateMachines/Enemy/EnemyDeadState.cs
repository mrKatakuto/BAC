using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    void Start() 
    {
        //SoundManager.Instance.Init();
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.Weapon.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);

        stateMachine.StartCoroutine(DestroyAfterDelay(3f));

        
    }

    public override void Tick(float deltaTime)
    {

    }

      public override void Exit()
    {

    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.Dead);
        yield return new WaitForSeconds(delay);  // Wartet für die angegebene Zeit
        GameObject.Destroy(stateMachine.Enemy);  // Zerstört das Ziel-GameObject
    }
}
