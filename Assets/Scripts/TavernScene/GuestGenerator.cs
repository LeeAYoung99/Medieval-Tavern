using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestGenerator : MonoBehaviour
{
    int[] samplenum = new int[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
    public static int indexCount = 0;
    public static int currentNum = 0;

    public GameObject MaleGuest;
    public GameObject FemaleGuest;
    bool isGenerateOn = false;

    private IEnumerator guestCo;

    // Start is called before the first frame update
    void Start()
    {
        /*
         * Knuth Shuffle Algorithm
         */
        for (int i = 0; i < samplenum.Length-1; i++)
        {
            int j = Random.Range(i, samplenum.Length);
            int tmp;
            tmp = samplenum[i];
            samplenum[i] = samplenum[j];
            samplenum[j] = tmp;
        }
        guestCo = Generate();
        StartCoroutine(guestCo);
    }

    IEnumerator Generate()
    {
        while (indexCount < 16)
        {
            

            isGenerateOn = true;
            //float randTime = 2.0f;
            float randTime = Random.Range(5.0f, 20.0f);
            yield return new WaitForSeconds(randTime);


            int randGuest = Random.Range(0, 2);
            currentNum = samplenum[indexCount];
            if (randGuest == 0)
            {
                Instantiate(MaleGuest, transform.position, transform.rotation);
            }
            else if (randGuest == 1)
            {
                Instantiate(FemaleGuest, transform.position, transform.rotation);
            }
         
            indexCount++;
            isGenerateOn = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariable.isEnding == true)
        {
            StopCoroutine(guestCo);
        }
        else if (GlobalVariable.currentGuestNum>=16)
        {
            StopCoroutine(guestCo);
        }
        else if(GlobalVariable.currentGuestNum < 16)
        {
            if (indexCount == 16)
            {
                indexCount = 0;
            }

            if (isGenerateOn == false)
            {
                StartCoroutine(guestCo);
            }
            
        }
    }


}
