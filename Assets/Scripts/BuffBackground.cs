using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBackground : MonoBehaviour
{
    public GameObject Description;

    void OnMouseEnter()
    {
        Description.SetActive(true);
    }

    void OnMouseExit()
    {
        Description.SetActive(false);
    }

}
