using UnityEngine;
using UnityEngine.UI;

public class RobotInfoBoxState : RobotBaseState
{
    private GameObject infoPanel;  
    private float activationDistance = 5.0f;  

    public RobotInfoBoxState(RobotStateMachine stateMachine, GameObject panel) : base(stateMachine)
    {
        infoPanel = panel;
    }

    public void Start() 
    {
        SoundManager.Instance.Init();
        SoundManager.Instance.PlaySound(SoundManager.Sound.Robot);
    }

    public override void Enter()
    {

        infoPanel.SetActive(false);
    }

    public override void Tick(float deltaTime)
    {
        float distanceToPlayer = Vector3.Distance(stateMachine.PlayerTransform.position, stateMachine.transform.position);
        
        if (distanceToPlayer <= activationDistance && !infoPanel.activeInHierarchy)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Robot);
            ShowInfoPanel();
            PauseGame();
        }
        else if (distanceToPlayer > activationDistance && infoPanel.activeInHierarchy)
        {
            CloseInfoPanel();
            ResumeGame();
        }
    }

    private void ShowInfoPanel()
    {
        infoPanel.SetActive(true);
        Button closeButton = infoPanel.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(OnCloseButtonPressed);
    }

    private void OnCloseButtonPressed()
    {
        CloseInfoPanel();
        ResumeGame();
        stateMachine.SwitchState(new RobotFollowState(stateMachine));
    }

    private void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
        Button closeButton = infoPanel.GetComponentInChildren<Button>();
        closeButton.onClick.RemoveListener(OnCloseButtonPressed);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; 
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // immer darauf achten das timescale auf 1 gesetzt wird
    }

    

    public override void Exit()
    {
        if (infoPanel.activeInHierarchy)
        {
            CloseInfoPanel();
            ResumeGame();
        }
    }
}
