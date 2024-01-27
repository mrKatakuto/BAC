using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextboxManager : MonoBehaviour
{
    public GameObject textBox1;
    public GameObject textBox2;
    public GameObject textBox3;
    
    void Start()
    {
        textBox2.SetActive(false);
        textBox3.SetActive(false);

        if (PlayerPrefs.HasKey("PlayerPositionX"))
        {
            textBox1.SetActive(false);
        }
        else
        {
            StartCoroutine(ShowTextbox1AfterDelay(5f));

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textBox2.SetActive(true);
            
        }
    }

    private IEnumerator ShowTextbox1AfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);


        textBox1.SetActive(true);
    }

}
