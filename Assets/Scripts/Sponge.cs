using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponge : MonoBehaviour
{

    float distance = 3;
    Vector3 beforePos;
    Vector3 currentPos;

    float _time = 0f;
    GameObject spoil;

    int number = 20;

    bool isTouching = false;

    public GameObject bubblePrefab;

    //bool isRunning = false;

    void Start()
    {
        beforePos = transform.position;
        currentPos = transform.position;
        spoil = GameObject.Find("Plate").transform.Find("Spoil").gameObject;
        
    }

    void FixedUpdate()
    {
        Debug.Log(number);
        Debug.Log(isTouching);
        _time += Time.deltaTime;
        currentPos = transform.position;

        spoil.GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, (float)number/20.0f);

        if (spoil.activeSelf == false)
        {
            _time = 0f;
            return;
        }
        if (number <= 0)
        {
            spoil.SetActive(false);
        }
        else
        if (_time > 0.3f)
        {
            
            if (beforePos != currentPos)
            {
                if (isTouching)
                {
                    Instantiate(bubblePrefab, transform.position, transform.rotation);
                    number--;
                }
                
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
        if (col.tag == "Plate")
        {
            isTouching = true;
            
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Plate")
        {
            isTouching = false;

        }
    }
}
