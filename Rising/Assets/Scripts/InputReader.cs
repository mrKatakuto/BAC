using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue {  get; private set; } 

    // Events for the actions everyone who listens will react
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

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }
}
