using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairInfo : MonoBehaviour
{
    const int chairNum = 16;

    public struct Chair
    {
        public Vector3 pos;
        public Quaternion rot;
        public GameObject allocatedCharacter;
    }

    public static Chair[] chairs = new Chair[chairNum];
    public GameObject[] ChairObject = new GameObject[chairNum];

    void Awake()
    {
        for (int i = 0; i < chairNum; i++)
        {
            chairs[i].pos = ChairObject[i].transform.position;
            chairs[i].rot = ChairObject[i].transform.rotation;
            chairs[i].allocatedCharacter = null;
        }
        
    }
   

}
