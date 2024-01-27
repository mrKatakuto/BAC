using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    void Start()
    {
        
        if (PlayerPrefs.HasKey("NextSpawnPoint"))
        {
            
            string spawnPointName = PlayerPrefs.GetString("NextSpawnPoint");
            Transform spawnPoint = GameObject.Find(spawnPointName).transform;

            
            if (spawnPoint != null)
            {
                transform.position = spawnPoint.position;
            }
            else
            {
                Debug.LogError("Spawnpoint " + spawnPointName + " nicht gefunden.");
            }

            
            PlayerPrefs.DeleteKey("NextSpawnPoint");
        }
    }
}
