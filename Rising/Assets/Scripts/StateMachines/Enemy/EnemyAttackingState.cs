using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private const float TransitionDuration = 0.1f;
    private bool soundPlayed = false;  

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
        soundPlayed = false; 
    }

    public override void Tick(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        
        if (normalizedTime >= 0.5f && !soundPlayed)  
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Sword, 0.15f); 
            soundPlayed = true;
        }

        // Prüfe, ob die Animation beendet ist
        if (normalizedTime >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }

        FacePlayer();
    }

    public override void Exit()
    {
        // Aufräumen, wenn der Zustand verlassen wird
    }
}
