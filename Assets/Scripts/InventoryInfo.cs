using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInfo : MonoBehaviour
{
    // 여기서부터는 실제로 보유하고 있는 아이템 목록입니다.
    public static int money = 999999;

    // 보드게임
    public static bool resistance = false;
    public static bool tabula = false;
    public static bool boom = false;

    // 음식재료
    public static int turkey = 999;
    public static int yogg = 999;
    public static int mushroom = 999;
    public static int berry = 999;
    public static int dragon = 999;
    public static int bread = 999;
    public static int banana = 999;


    //여기까지 아이템 목록이었습니다.

    public GameObject EffectPrefab;
    public Transform Canvas;
    public static int EffectMoney = 0;//이펙트 함수에 전달될 수치

    /// <summary>
    /// 
    /// </summary>
    public Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoneyTextUpdate();
        
        
    }

    void MoneyTextUpdate()
    {
        moneyText.text = "Gold: " + money.ToString();
    }
    
    public void MoneyGainEffect(int money)
    {
        EffectMoney = money;
        GameObject _effect = Instantiate(EffectPrefab, new Vector3(transform.position.x, transform.position.y+30.2f, transform.position.z), Quaternion.identity);
        _effect.gameObject.transform.SetParent(Canvas, false);
    }
    
    
}
