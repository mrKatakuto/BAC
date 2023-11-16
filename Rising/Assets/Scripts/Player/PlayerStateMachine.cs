using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    // jeder kann es lesen aber es kann nicht gesetzt werden von �berall
    [field: SerializeField] public InputReader InputReader { get; private set; }

    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public float  FreeLookMovementSpeed { get; private set; }

    [field: SerializeField] public float  RotationDamping { get; private set; }

    public Transform MainCameraTransform { get; private set; }

    void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }
}
