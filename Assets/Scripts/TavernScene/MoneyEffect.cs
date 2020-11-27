using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyEffect : MonoBehaviour
{
    //InventoryInfo 스크립트에서 Instantiate 동적생성됩니다.

    //MoneyGainEffect 함수에 쓰는 것들
    float moneyEffectTime;
    public float _moneyEffectFadeTime = 4.5f;
    float speed = 30.5f;
   

    // Start is called before the first frame update
    void Start()
    {
        moneyEffectTime = 0;
        if (InventoryInfo.EffectMoney < 0)
        {
            this.gameObject.GetComponent<Text>().text = InventoryInfo.EffectMoney.ToString();
        }
        else if (InventoryInfo.EffectMoney >= 0)
        {
            this.gameObject.GetComponent<Text>().text = "+" + InventoryInfo.EffectMoney.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoneyGainEffectUpdate();
    }

    void MoneyGainEffectUpdate()
    {
        if (moneyEffectTime < _moneyEffectFadeTime)
        {
            this.gameObject.GetComponent<Text>().color = new Color(1, 1, 1, 1f - moneyEffectTime / _moneyEffectFadeTime);
            this.gameObject.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
        else
        {
            Destroy(this.gameObject);  
        }
        moneyEffectTime += Time.deltaTime;
    }

}
