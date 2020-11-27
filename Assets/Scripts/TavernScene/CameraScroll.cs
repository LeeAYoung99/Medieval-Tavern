using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public Camera Cam;
    float speed = 400.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Scroll();
    }

    void Scroll()
    {
        Cam.transform.Translate(Input.GetAxis("Mouse ScrollWheel") * Vector3.forward * speed * Time.smoothDeltaTime);
    }
    
}
