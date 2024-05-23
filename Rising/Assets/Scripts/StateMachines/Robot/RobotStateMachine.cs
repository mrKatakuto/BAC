using UnityEngine;
public class RobotStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public Transform PlayerTransform {get; private set;}
    [field: SerializeField] public GameObject InfoPanel;

    [SerializeField] public float followDistance = 5.0f;

    private void Start() 
    {
        //wenn gplayer in reichweite ist dann wechsle in den display text modus 
        
        SwitchState(new RobotInfoBoxState(this, InfoPanel));
    }
}