using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObjectController : MonoBehaviour
{

    public GameObject Cheese;
    public GameObject Chicken;

    public GameObject[] table= new GameObject[4];
    bool[] isFoodOnTable = new bool[4];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            isFoodOnTable[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (ChairInfo.chairs[i].isAllocated == true)
            {
                if (isFoodOnTable[0]) break;
                isFoodOnTable[0] = true;
                Instantiate(Chicken, new Vector3(table[0].transform.position.x, table[0].transform.position.y + 3.3f, table[0].transform.position.z), table[0].transform.rotation);
                break;
            }
        }

        for (int i = 4; i < 8; i++)
        {
            if (ChairInfo.chairs[i].isAllocated == true)
            {
                if (isFoodOnTable[1]) break;
                isFoodOnTable[1] = true;
                Instantiate(Chicken, new Vector3(table[1].transform.position.x, table[1].transform.position.y+3.3f, table[1].transform.position.z), table[1].transform.rotation);
                break;
            }
        }

        for (int i = 8; i < 12; i++)
        {
            if (ChairInfo.chairs[i].isAllocated == true)
            {
                if (isFoodOnTable[2]) break;
                isFoodOnTable[2] = true;
                Instantiate(Chicken, new Vector3(table[2].transform.position.x, table[2].transform.position.y + 3.3f, table[2].transform.position.z), table[2].transform.rotation);
                break;
            }
        }

        for (int i = 12; i < 16; i++)
        {
            if (ChairInfo.chairs[i].isAllocated == true)
            {
                if (isFoodOnTable[3]) break;
                isFoodOnTable[3] = true;
                Instantiate(Chicken, new Vector3(table[3].transform.position.x, table[3].transform.position.y + 3.3f, table[3].transform.position.z), table[3].transform.rotation);
                break;
            }
        }


    }
}
