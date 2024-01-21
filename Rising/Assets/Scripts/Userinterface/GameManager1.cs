using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance { get; private set; }
    public GameObject player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Debug.Log(gameObject.name + " position: " + transform.position);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Wenn eine neue Spiel-Szene geladen wird, finde den Spieler
        if (scene.name != "Main_Menu") // Ersetze "Main_Menu" durch den tatsächlichen Namen deiner Hauptmenü-Szene
        {
            FindPlayer();
        }
    }

    public void SaveGame()
    {
        if (player == null)
        {
            FindPlayer();
        }

        if (player != null)
        {
            PlayerPrefs.SetFloat("PlayerPositionX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPositionY", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerPositionZ", player.transform.position.z);
            PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();

            Debug.Log("Saved Scene: " + PlayerPrefs.GetString("CurrentLevel"));
            Debug.Log("Saved Position: " + new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"), PlayerPrefs.GetFloat("PlayerPositionY"), PlayerPrefs.GetFloat("PlayerPositionZ")));
        }
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            string levelToLoad = PlayerPrefs.GetString("CurrentLevel");

            Debug.Log("Loading level: " + levelToLoad);

            SceneManager.LoadScene(levelToLoad);
            StartCoroutine(WaitForSceneLoad());
        }
    }

    private IEnumerator WaitForSceneLoad()
    {
        while (SceneManager.GetActiveScene().name != PlayerPrefs.GetString("CurrentLevel"))
        {
            yield return null; // Warte einen Frame
        }

        FindPlayer();

        if (player != null)
        {
            SetPlayerPosition();
            Debug.Log("Player position set: " + player.transform.position);
        }
        else
        {
            Debug.LogError("Player not found in the loaded scene.");
        }
    }

    private void SetPlayerPosition()
    {
        if (player != null && PlayerPrefs.HasKey("PlayerPositionX"))
        {
            // Deaktiviere den Charakter-Controller, falls vorhanden
            CharacterController charController = player.GetComponent<CharacterController>();

            if (charController != null)
            {
                charController.enabled = false;
            }

            float x = PlayerPrefs.GetFloat("PlayerPositionX");
            float y = PlayerPrefs.GetFloat("PlayerPositionY");
            float z = PlayerPrefs.GetFloat("PlayerPositionZ");
            player.transform.position = new Vector3(x, y, z);

            Debug.Log("Player position set for: " + player.name + " to position: " + player.transform.position);

            // Aktiviere den Charakter-Controller wieder
            if (charController != null)
            {
                charController.enabled = true;
            }
        }
        else if (player == null)
        {
            Debug.LogError("SetPlayerPosition: Player object not found.");
        }
        else
        {
            Debug.LogError("SetPlayerPosition: Player position keys not found in PlayerPrefs.");
        }
    }

    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
