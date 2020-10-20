using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOwned : MonoBehaviour
{
    /// <summary>
    /// 이 스크립트는 플레이어가 가지고 있는 것을 나타내는 UI입니다.
    /// </summary>

    public GameObject MyImage;//말풍선 위에 있는 이미지
    private GameObject PlayerLocation;//플레이어 위치만 가져올거임!
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerLocation = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(PlayerLocation.transform.position + new Vector3(3.0f, 2.8f, 2.0f)); //플레이어 위치에 맞추어이동
        ChangeImage();
      
    }

    void ChangeImage()
    {
        if (Player.playerOwnedFood == UIController.Food.Stick)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/꼬치");
        }
        else if (Player.playerOwnedFood == UIController.Food.WitchSoup)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/마녀의수프");
        }
        else if (Player.playerOwnedFood == UIController.Food.BerrySandwich)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/딸기샌드위치");
        }
        else if (Player.playerOwnedFood == UIController.Food.WingSalad)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/날개샐러드");
        }
        else if (Player.playerOwnedFood == UIController.Food.Spoiled)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/망한요리");
        }
        else if (Player.playerOwnedFood == UIController.Food.RoastedTurkey)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/칠면조정식");
        }
        else if (Player.playerOwnedFood == UIController.Food.Nothing)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/VoidImage");
        }
    }
}
