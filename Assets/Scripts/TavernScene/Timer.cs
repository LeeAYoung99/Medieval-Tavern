using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimeText;
    public static int OneSecond = 0;
    public int hour = 17;
    public int min = 0;

    public GameObject ResultPrefab;
    public Transform ResultParent;

    ResultController resultController;

    private IEnumerator timerCo;
    // Start is called before the first frame update
    void Start()
    {
        resultController = GameObject.Find("ResultController").GetComponent<ResultController>();
        timerCo = MyTimer();
        StartCoroutine(timerCo);
    }

    // Update is called once per frame
    void Update()
    {
        if (min < 10)
        {
            TimeText.text = hour.ToString() + ":0" + min.ToString();
        }
        else
        {
            TimeText.text = hour.ToString() + ":" + min.ToString();
        }

        if (hour == 21 && min == 0)
        {
            GlobalVariable.isEnding = true;
            resultController.EndMoney = InventoryInfo.money;
            resultController.EndRep = InventoryInfo.reputation;
            GameObject _result = Instantiate(ResultPrefab, new Vector2(0.0f, 0.0f), Quaternion.identity);
            _result.gameObject.transform.SetParent(ResultParent, false);
        }

        if (GlobalVariable.isEnding == true)
        {
            StopCoroutine(timerCo);
        }
        


    }

    IEnumerator MyTimer()
    {
        while(true)
        {

            yield return new WaitForSeconds(1f);
            
            OneSecond++;
            min++;

            if (min >= 60)
            {
                min = 0;
                hour++;
            }
            if (hour >= 24)
            {
                hour = 0;
            }
        }
        
    }
    
}
