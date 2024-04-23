using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 90.0f; // Geschwindigkeit der Rotation in Grad pro Sekunde

    void Update()
    {
        // Kontinuierliche Rotation um die Y-Achse
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
