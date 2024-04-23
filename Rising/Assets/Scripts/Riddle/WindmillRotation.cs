using UnityEngine;

public class WindmillRotation : MonoBehaviour
{
    public float rotationSpeed = 50.0f;  
    public bool isRotating = false;     

    void Update()
    {

        if (!isRotating)
        {
            CheckForActivationInput();
        }


        if (isRotating)
        {
            RotateWindmill();
        }
    }

    void CheckForActivationInput()
    {
        if (Input.GetKeyDown(KeyCode.F))  
        {
            isRotating = true;  
        }
    }

    void RotateWindmill()
    {
        transform.Rotate(new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
    }


    public void ActivateRotation()
    {
        isRotating = true;
    }
}
