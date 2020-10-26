using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotOwned : MonoBehaviour
{
    /// <summary>
    /// 이 스크립트는 플레이어가 가지고 있는 것을 나타내는 UI입니다.
    /// </summary>

    public GameObject MyImage;//말풍선 위에 있는 이미지
    private GameObject Cook;//플레이어 위치만 가져올거임!

    private GameObject MyCamera;


    public float minimumDist = 23.0f;
    public float maximumDist = 80.0f;
    float camToCookDistance;

    // Start is called before the first frame update
    void Start()
    {
        MyCamera = GameObject.Find("Main Camera");
        Cook = GameObject.Find("FoodKit");
        camToCookDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(Cook.transform.position + new Vector3(3.0f, 2.8f, 2.0f)); //플레이어 위치에 맞추어이동
        ChangeImage();
        ChangeImageTransparent();

    }

    void ChangeImage()
    {
        if (UIController.PotFood == UIController.Food.Stick)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/꼬치");
        }
        else if (UIController.PotFood == UIController.Food.WitchSoup)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/마녀의수프");
        }
        else if (UIController.PotFood == UIController.Food.BerrySandwich)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/딸기샌드위치");
        }
        else if (UIController.PotFood == UIController.Food.WingSalad)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/날개샐러드");
        }
        else if (UIController.PotFood == UIController.Food.Spoiled)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/망한요리");
        }
        else if (UIController.PotFood == UIController.Food.RoastedTurkey)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/칠면조정식");
        }
        else if (UIController.PotFood == UIController.Food.Nothing)
        {
            MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/VoidImage");
        }
       
    }

    void ChangeImageTransparent()
    {
        //거리에 비례해서 슬라이더 투명도 다르게 하기
        camToCookDistance = Vector3.Distance(MyCamera.gameObject.transform.position, Cook.gameObject.transform.position);//거리 재기
        if (minimumDist < camToCookDistance && camToCookDistance < maximumDist)
        {
            MyImage.GetComponent<Image>().color = new Color(1, 1, 1, 1.0f - (camToCookDistance - minimumDist) / (maximumDist - minimumDist));
            this.GetComponent<Image>().color = new Color(1, 1, 1, (1.0f - (camToCookDistance - minimumDist) / (maximumDist - minimumDist))*0.8f);
        }
        else if (camToCookDistance <= 23)
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
