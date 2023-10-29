using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    // jeder kann es lesen aber es kann nicht gesetzt werden von überall
    [field: SerializeField] public InputReader InputReader { get; private set; }

    void Start()
    {
        SwitchState(new PlayerTestState(this));
    }
}
