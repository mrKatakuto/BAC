using UnityEngine;
using System.Collections;

public class TextboxManager : MonoBehaviour
{
    public GameObject textBox1;
    public GameObject textBox2;
    public GameObject textBox3;

    void Start()
    {
        // Alle Textboxen beim Start deaktivieren
        textBox1.SetActive(false);
        textBox2.SetActive(false);
        textBox3.SetActive(false);

        // Nur die erste Textbox anzeigen, wenn keine gespeicherte Spielerposition vorhanden ist
        if (!PlayerPrefs.HasKey("PlayerPositionX"))
        {
            StartCoroutine(ShowTextbox1AfterDelay(5f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Deaktiviere alle anderen Textboxen und aktiviere nur textBox3
            textBox1.SetActive(false);
            textBox2.SetActive(false);
            textBox3.SetActive(true);
        }
    }

    private IEnumerator ShowTextbox1AfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Stelle sicher, dass textBox2 und textBox3 deaktiviert sind, bevor textBox1 aktiviert wird
        textBox2.SetActive(false);
        textBox3.SetActive(false);
        textBox1.SetActive(true);
    }
}
