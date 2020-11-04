using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestGenerator : MonoBehaviour
{
    int[] samplenum = new int[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
    public static int indexCount = 0;
    public static int currentNum = 0;

    public GameObject Guest;

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
        StartCoroutine("Generate");
    }

    IEnumerator Generate()
    {
        while (indexCount < 16)
        {
            float randTime = Random.Range(6.0f, 20.0f);
            yield return new WaitForSeconds(randTime);

            currentNum = samplenum[indexCount];
            Instantiate(Guest, transform.position, transform.rotation);
            indexCount++;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }


}
