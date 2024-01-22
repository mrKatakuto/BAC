using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance { get; private set; }
    public GameObject player;
    private InputReader inputReader;

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

    private void Start()
    {
        inputReader = FindObjectOfType<InputReader>();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Main_Menu") 
        {
            FindPlayer();
        }
    }

    public void SaveGame()
    {
        if (player != null)
        {
            PlayerPrefs.SetFloat("PlayerPositionX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPositionY", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerPositionZ", player.transform.position.z);
            PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
        }
    }

    public void LoadGame()
    {
        inputReader.enabled = true;

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            string levelToLoad = PlayerPrefs.GetString("CurrentLevel");
            SceneManager.LoadScene(levelToLoad);
            StartCoroutine(WaitForSceneLoad());
        }
    }

    private IEnumerator WaitForSceneLoad()
    {
        while (SceneManager.GetActiveScene().name != PlayerPrefs.GetString("CurrentLevel"))
        {
            yield return null;
        }

        FindPlayer();
        if (player != null)
        {
            SetPlayerPosition();
        }
    }

    private void SetPlayerPosition()
    {
        if (player != null && PlayerPrefs.HasKey("PlayerPositionX"))
        {
            CharacterController charController = player.GetComponent<CharacterController>();
            if (charController != null) charController.enabled = false;

            float x = PlayerPrefs.GetFloat("PlayerPositionX");
            float y = PlayerPrefs.GetFloat("PlayerPositionY");
            float z = PlayerPrefs.GetFloat("PlayerPositionZ");
            player.transform.position = new Vector3(x, y, z);

            if (charController != null) charController.enabled = true;
        }
    }

    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}