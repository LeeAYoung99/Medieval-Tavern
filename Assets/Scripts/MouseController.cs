using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    //이거 마우스 움직이는게 아니라 진짜 쥐 움직이는 거임...

    Timer timer;
    bool isGenerated = false;
    public GameObject MouseCharacter;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("DayAndTime").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.hour == 17 && timer.min == 30 && !isGenerated)
        {
            isGenerated = true;
            Instantiate(MouseCharacter, transform.position, transform.rotation);
        }
    }
}
