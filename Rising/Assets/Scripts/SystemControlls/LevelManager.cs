using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string nextLevelSceneName;
    public Loader levelLoader; 
    public string loadingScreenName; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelLoader.LoadLevel(nextLevelSceneName, loadingScreenName);
        }
    }
}

