using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string nextLevelSceneName; 
    public Transform spawnPoint; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerPrefs.SetString("NextSpawnPoint", spawnPoint.name); 
            SceneManager.LoadScene(nextLevelSceneName);
        }
    }
}
