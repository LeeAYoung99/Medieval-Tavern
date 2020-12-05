using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 2.0f)
            time += Time.deltaTime;

        if (transform.position.y < Camera.main.pixelHeight / 2)
            transform.Translate(new Vector2(0, 1)* Time.deltaTime * 200.0f);

        if (time < 2.0f)
            this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, time / 2.0f);


    }
}
