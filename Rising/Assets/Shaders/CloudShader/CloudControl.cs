using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CloudControl : MonoBehaviour
{

    private float timer = 4f;
    [SerializeField]
    private MeshRenderer _mr;
    private float XValue=-0.001f;
    private float YValue=-0.03f;
    private int _cloudHeight = 15;
    bool up;



    // Start is called before the first frame update
    void Start()
    {
        up = false;
        _mr.material.SetVector("_Cloud_speed", new Vector2(XValue, YValue));
        _mr.material.SetVector("_Cloud_Height", new Vector3(0, _cloudHeight,0));

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer<=2)
        {
            SetCloudDirection();
            SetCloudHeight();
            timer = 4;
        }
        //Debug.Log(timer);
    }


    private void SetCloudDirection()
    {
      
        if (up)
        {
            XValue += 0.001f;
            YValue += 0.001f;         
        }

        if (!up)
        {
            XValue -= 0.001f;
            YValue -= 0.001f;        
        }
       
        _mr.material.SetVector("_Cloud_speed", new Vector2(XValue, YValue));

    }

    private void SetCloudHeight()
    {
        if (up)
        {
            _cloudHeight--;
            if (_cloudHeight < -15)
            {
                
                up = false;
            }

        }
       if(!up)
        {
            _cloudHeight++;
            if (_cloudHeight > 15)
            {
                up = true;
            }
        }
        
        _mr.material.SetVector("_Cloud_Height", new Vector3(0, _cloudHeight, 0));
    }
}
