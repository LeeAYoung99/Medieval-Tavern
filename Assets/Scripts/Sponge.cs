using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponge : MonoBehaviour
{

    float distance = 5;
    Vector3 beforePos;
    Vector3 currentPos;

    float _time = 0f;

    public GameObject bubblePrefab;

    bool isRunning = false;

    void Start()
    {
        beforePos = transform.position;
        currentPos = transform.position;
    }

    void Update()
    {
        _time += Time.deltaTime;
        currentPos = transform.position;

        if (_time > 0.3f)
        {
            if (beforePos != currentPos)
            {
                Instantiate(bubblePrefab, transform.position, transform.rotation);
            }
            _time = 0f;
        }
       
        
        beforePos = transform.position;
    }


    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Spoil")
        {

        }
    }
}
