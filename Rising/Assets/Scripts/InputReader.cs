using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    // Events for the actions
    public event Action JumpEvent;
    public event Action DodgeEvent;

    private Controls controls;

    void Start()
    {
        controls = new Controls();

        // reference to the actual input methods (action maps)
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // trigger event
        if (!context.performed) { return; }
        {
            JumpEvent?.Invoke();
        }

    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        {
            DodgeEvent?.Invoke();
        }
    }
}
