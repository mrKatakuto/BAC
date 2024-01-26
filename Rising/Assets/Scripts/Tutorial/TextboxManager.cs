using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextboxManager : MonoBehaviour
{
    public GameObject textBox;
    public Button closeButton;

    void Start()
    {
        textBox.SetActive(false);

        StartCoroutine(ShowTextboxAfterDelay(5f));
    }

    private IEnumerator ShowTextboxAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);


        textBox.SetActive(true);
    }
}
