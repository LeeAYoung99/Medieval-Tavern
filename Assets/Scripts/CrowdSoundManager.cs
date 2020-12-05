using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSoundManager : MonoBehaviour
{
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariable.currentGuestNum > 2)
        {
            audio.volume = 0.5f + (float)GlobalVariable.currentGuestNum / 32.0f;
            
        }
    }
}
