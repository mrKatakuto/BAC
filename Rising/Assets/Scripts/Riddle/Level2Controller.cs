using UnityEngine;
using UnityEngine.UI;

public class Level2Controller : MonoBehaviour
{
    public GameObject infoPanel; 
    public RobotStateMachine robotStateMachine; 

    private bool isFirstTime = true; 

    void Start()
    {
        infoPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isFirstTime)
        {
            infoPanel.SetActive(true); 
            isFirstTime = false; 
            robotStateMachine.enabled = false; 
        }
    }

    public void CloseInfoPanel()
    {
  
        infoPanel.SetActive(false); 
        robotStateMachine.enabled = true; 
    }
}
