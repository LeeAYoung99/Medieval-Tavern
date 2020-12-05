using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGenerator : MonoBehaviour
{
    /*
     * 쓰레기를 만드는 스크립트 
     */

    public Vector3[] randpos = new Vector3[5];
    public GameObject trashPrefab;
    public GameObject trashPrefab2;
    public bool[] randposBool = new bool[5];
    

    // Start is called before the first frame update
    void Start()
    {
        randpos[0] = new Vector3(20.63f, 0.43f, -19.55f);
        randpos[1] = new Vector3(18.7f, 0.43f, -30.41f);
        randpos[2] = new Vector3(2.58f, 0.43f, -23.06f);
        randpos[3] = new Vector3(-5.93f, 0.43f, -19.46f);
        randpos[4] = new Vector3(14.5f, 0.43f, -4.4f);

        for (int i = 0; i < 5; i++)
        {
            randposBool[i] = false;
        }

        StartCoroutine("Generator");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Generator()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            int percent1 = 0;
            int percent2 = 0;
            int percent3 = 0;

            if (GlobalVariable.currentGuestNum > 2)
            {
                percent1 = Random.Range(0, 5); //0,5
                if (percent1 == 0)
                {
                    percent2 = Random.Range(0, 5);
                    if (randposBool[percent2] == false)
                    {
                        randposBool[percent2] = true;

                        percent3 = Random.Range(0, 2);
                        if (percent3 == 0)
                            Instantiate(trashPrefab, randpos[percent2], transform.rotation);
                        else
                            Instantiate(trashPrefab2, randpos[percent2], transform.rotation);
                       
                    }
                }
            }
            int dirtyness = 0;
            int randnum = 0;
            for (int i = 0; i < 5; i++)
            {
                if (randposBool[i] == true) dirtyness++;
            }
            if (dirtyness >= 2)
            {
                randnum = Random.Range(0, 5);
                if (randnum == 0)
                {
                    if (InventoryInfo.reputation - 1 >= -10 && InventoryInfo.reputation - 1 <= 10) InventoryInfo.reputation--;
                }
            }

        }

    }
}
