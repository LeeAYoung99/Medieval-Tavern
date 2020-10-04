using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //Screen.sleepTimeOut = SleepTimeout.NeverSleep;//화면안꺼지게하는함수
        Screen.SetResolution(2048, 1152, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
