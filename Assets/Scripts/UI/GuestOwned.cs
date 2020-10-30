using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestOwned : MonoBehaviour
{

    public GameObject MyImage;//말풍선 위에 있는 이미지
    /// <summary>
    /// 주의!  transform.root를 사용했으므로 생성되는 게스트 캐릭터는 부모 오브젝트가 없도록 하세요!
    /// </summary>

    float camToGuestDistance;

    private GameObject MyCamera;

    public float minimumDist = 23.0f;
    public float maximumDist = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        camToGuestDistance = 0;

        MyCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(transform.root.transform.position + new Vector3(3.0f, 2.8f, 2.0f)); //플레이어 위치에 맞추어이동
        //ChangeImage();
        ChangeUITransparent();

    }
    /*
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
        else if (Player.playerOwnedFood == UIController.Food.Beer)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/오크통맥주");
        }
        else if (Player.playerOwnedFood == UIController.Food.SlimeCocktail)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/슬라임칵테일");
        }
        else if (Player.playerOwnedFood == UIController.Food.Wine)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/와인");
        }
    }*/


    void ChangeUITransparent()
    {
        //거리에 비례해서 슬라이더 투명도 다르게 하기
        camToGuestDistance = Vector3.Distance(MyCamera.gameObject.transform.position, transform.root.transform.position);//거리 재기
        if (minimumDist < camToGuestDistance && camToGuestDistance < maximumDist)
        {
            MyImage.GetComponent<Image>().color = new Color(1, 1, 1, 1.0f - (camToGuestDistance - minimumDist) / (maximumDist - minimumDist));
            this.GetComponent<Image>().color = new Color(1, 1, 1, (1.0f - (camToGuestDistance - minimumDist) / (maximumDist - minimumDist)) * 0.8f);
        }
        else if (camToGuestDistance <= 23)
        {
            MyImage.GetComponent<Image>().color = new Color(1, 1, 1, 1.0f);
            this.GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
        }
        else
        {
            MyImage.GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);
            this.GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);
        }
    }
}
