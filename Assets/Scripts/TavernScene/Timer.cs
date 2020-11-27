using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimeText;
    public static int OneSecond = 0;
    int hour = 17;
    int min = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("MyTimer");
    }

    // Update is called once per frame
    void Update()
    {
        if (min < 10)
        {
            TimeText.text = hour.ToString() + ":0" + min.ToString();
        }
        else
        {
            TimeText.text = hour.ToString() + ":" + min.ToString();
        }


    }

    IEnumerator MyTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            OneSecond++;
            min++;

            if (min >= 60)
            {
                min = 0;
                hour++;
            }
            if (hour >= 24)
            {
                hour = 0;
            }
        }
        
    }
    
}
