using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera Cam;
    float speed = 10.0f;
    bool leftandfront = false;
    bool rightandfront = false;
    bool leftandback = false;
    bool rightandback = false;
    bool left = false;
    bool front = false;
    bool back = false;
    bool right = false;

    //마우스 이동 범위
    float maximumX = 22.0f;
    float maximumZ = 1.0f;
    float minimumX = -13.0f;
    float minimumZ = -51.0f;

    Vector3 BeforePosition;

    // Start is called before the first frame update
    void Start()
    {
        BeforePosition = Cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GlobalVariable.isUIOn()) //UI가 꺼져있다면
        {
            CameraMouseOver(); //화면에 마우스를 올려놓아 움직이는 것을 허용한다
        }
        RestrictCameraArea();
        
    }

    void RestrictCameraArea()
    {
        if (Cam.transform.position.x < minimumX)
        {
            Vector3 temp = Cam.transform.position;
            temp.x = minimumX;
            Cam.transform.position = temp;
        }
        if (Cam.transform.position.x > maximumX)
        {
            Vector3 temp = Cam.transform.position;
            temp.x = maximumX;
            Cam.transform.position = temp;
        }
        if (Cam.transform.position.z < minimumZ)
        {
            Vector3 temp = Cam.transform.position;
            temp.z = minimumZ;
            Cam.transform.position = temp;
        }
        if (Cam.transform.position.z > maximumZ)
        {
            Vector3 temp = Cam.transform.position;
            temp.z = maximumZ;
            Cam.transform.position = temp;
        }
    }
 

    void CameraMouseOver()
    {
        if (leftandfront == true)
        {
            Cam.transform.Translate(Vector3.left * speed * Time.smoothDeltaTime, Space.World);
            Cam.transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime, Space.World);
        }
        if (rightandfront == true)
        {
            Cam.transform.Translate(Vector3.right * speed * Time.smoothDeltaTime, Space.World);
            Cam.transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime, Space.World);
        }
        if (leftandback == true)
        {
            Cam.transform.Translate(Vector3.left * speed * Time.smoothDeltaTime, Space.World);
            Cam.transform.Translate(Vector3.back * speed * Time.smoothDeltaTime, Space.World);
        }
        if (rightandback == true)
        {
            Cam.transform.Translate(Vector3.right * speed * Time.smoothDeltaTime, Space.World);
            Cam.transform.Translate(Vector3.back * speed * Time.smoothDeltaTime, Space.World);
        }
        if (left == true)
        {
            Cam.transform.Translate(Vector3.left * speed * Time.smoothDeltaTime, Space.World);
        }
        if (right == true)
        {
            Cam.transform.Translate(Vector3.right * speed * Time.smoothDeltaTime, Space.World);
        }
        if (front == true)
        {
            Cam.transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime, Space.World);
        }
        if (back == true)
        {
            Cam.transform.Translate(Vector3.back * speed * Time.smoothDeltaTime, Space.World);
        }
    }

    public void LeftAndFrontEnter()
    {
        leftandfront = true;
    }

    public void LeftAndFrontExit()
    {
        leftandfront = false;
    }

    public void RightAndFrontEnter()
    {
        rightandfront = true;
    }

    public void RightAndFrontExit()
    {
        rightandfront = false;
    }

    public void LeftAndBackEnter()
    {
        leftandback = true;
    }

    public void LeftAndBackExit()
    {
        leftandback = false;
    }

    public void RightAndBackEnter()
    {
        rightandback = true;
    }

    public void RightAndBackExit()
    {
        rightandback = false;
    }

    ////////

    public void LeftEnter()
    {
        left = true;
    }

    public void LeftExit()
    {
        left = false;
    }

    public void FrontEnter()
    {
        front = true;
    }

    public void FrontExit()
    {
        front = false;
    }

    public void RightEnter()
    {
        right = true;
    }

    public void RightExit()
    {
        right = false;
    }

    public void BackEnter()
    {
        back = true;
    }

    public void BackExit()
    {
        back = false;
    }


}
