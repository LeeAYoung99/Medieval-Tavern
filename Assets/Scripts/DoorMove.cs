using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    public GameObject Door;
    int currentCollideGuest;
    // Start is called before the first frame update
    void Start()
    {
        currentCollideGuest = 0;
    }

    // Update is called once per frame
    void Update()
    { 
        if(currentCollideGuest > 0)
        {
            if (Door.transform.rotation.z >= -0.7)
                Door.transform.RotateAround(new Vector3(-14.14f, -0.1059799f, -36.25f), Vector3.up, -50 * Time.deltaTime);
        }
        else
        {
            if (Door.transform.rotation.z <= -0.5)
                Door.transform.RotateAround(new Vector3(-14.14f, -0.1059799f, -36.25f), Vector3.up, 50 * Time.deltaTime);
        }
        Debug.Log(Door.transform.rotation.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Guest")
        {
            currentCollideGuest++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Guest")
        {
            currentCollideGuest--;
        }
    }

}
