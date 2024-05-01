using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStateMachine : StateMachine
{
    // jeder kann es lesen aber es kann nicht gesetzt werden von �berall
    [field: SerializeField] public InputReader InputReader { get; private set; }

    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public Targeter  Targeter { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver {get;private set;}

    [field: SerializeField] public WeaponDamage Weapon {get;private set;}

    [field: SerializeField] public Health Health {get;private set;}

    [field: SerializeField] public Ragdoll Ragdoll {get;private set;}

    [field: SerializeField] public LedgeDetector LedgeDetector {get;private set;}

    [field: SerializeField] public float  FreeLookMovementSpeed { get; private set; }
    
    [field: SerializeField] public float  TargetingMovementSpeed { get; private set; }

    [field: SerializeField] public float  RotationDamping { get; private set; }

    [field: SerializeField] public float  DodgeDuration { get; private set; }

    [field: SerializeField] public float  DodgeLength { get; private set; }

    [field: SerializeField] public float  JumpForce { get; private set; }

    [field: SerializeField] public Attack[] Attacks { get; private set; }

    public Transform MainCameraTransform { get; private set; } 

    // Um den ersten dodge cooldown zu garantieren (Mathf.)
    public float PreviousDodgeTime {get; private set; } = Mathf.NegativeInfinity;

    void Start()
    {
        // Für cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        
        MainCameraTransform = Camera.main.transform;

        SetInitialState();

        /*if (PlayerPrefs.HasKey("CurrentLevel") || PlayerPrefs.HasKey("PlayerPositionX"))
        {
            string currentLevel = PlayerPrefs.GetString("CurrentLevel");
            float x = PlayerPrefs.GetFloat("PlayerPositionX");

            if (currentLevel == "Level_1_The_Discovery" & x > 0)
            {

                // 23.01. hinzu und das hier aktivieren
                SwitchState(new PlayerSittingState(this));
            }
            else
            {
                // 23.01. auskomm
                SwitchState(new PlayerFreeLookState(this));
            }   
        }
        */
        

        // 23.01. hinzu falls fehler auftreten das hier deaktiviert lassen
        //SwitchState(new PlayerSittingState(this));   
    }
    // hinzu 27.01
    private void SetInitialState()
    {
        if (SceneManager.GetActiveScene().name == "Level_2_Neu")
        {
            SwitchState(new PlayerFreeLookState(this));
        }
        else if (PlayerPrefs.HasKey("PlayerPositionX"))
        {
            SwitchState(new PlayerFreeLookState(this));
        }
        else
        {
            SwitchState(new PlayerSittingState(this));
        }
    }
        private void OnEnable() 
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnTakeDamage() 
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void HandleTakeDamage() 
    {
        SwitchState(new PlayerImpactState(this));
    }

        private void HandleDie() 
    {
        SwitchState(new PlayerDeadState(this));
    }
}
