﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// 이 스크립트는 UI들을 통합적으로 관리해주는 스크립트입니다.
    /// </summary>

    public GameObject BoardGameUI; //실제 보드게임 UI 오브젝트 
    public GameObject CookUI; //실제 쿡 UI 오브젝트 

    //요리가 지금 되고 있나 아닌가 체크!
    public enum CookingPotState { isPotEmpty, isCooking, isFoodReady }; //PotEmpty: 비어있을때. isCooking:요리중이라 게이지 올라감. FoodReady: 요리가 준비되어서 가져갈수있음.
    public enum Food { KnightSoup, BerrySandwich, WingSalad, WitchSoup, RoastedTurkey };
    public static CookingPotState CookingState; //지금 팟 안에는 어떤 상태일까?
    public static Food PotFood; //팟 안에 어떤 음식이 들어있을까?
    float cookingTime; //요리중일때 시간 돌아감.

    //레지스탕스
    public Text ResistanceBuyText;
    public GameObject ResistanceButton;
    public GameObject ResistanceImage;

    //타뷸라
    public Text TabulaBuyText;
    public GameObject TabulaButton;
    public GameObject TabulaImage;

    //붐
    public Text BoomBuyText;
    public GameObject BoomButton;
    public GameObject BoomImage;


    //음식 텍스트 (개수 숫자)
    public Text turkeyText;
    public Text yoggText;
    public Text mushroomText;
    public Text berryText;
    public Text dragonText;
    public Text breadText;
    public Text bananaText;

    //음식 UI에 사용되는 넘들
    public enum CookItemType { nothing, turkey, yogg, mushroom, berry, dragon, bread, banana };
    public static CookItemType CookItemZoneLeft;//+기준 좌측
    public static CookItemType CookItemZoneRight;//+기준 우측
    public GameObject CookLeft;
    public GameObject CookRight;
    
    private InventoryInfo InventoryInfoScript;


    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        CookingState = CookingPotState.isPotEmpty;
        cookingTime = 0;
        InventoryInfoScript = GameObject.Find("InventoryInfo").GetComponent("InventoryInfo") as InventoryInfo;
        CookItemZoneLeft = CookItemType.nothing;
        CookItemZoneRight = CookItemType.nothing;
        
    }

    // Update is called once per frame
    void Update()
    {
        UIActiveController();
        BoardGameUIController();
        CookUIController();
        CookingController();
        
    }

    void UIActiveController() //UI 액티브들을 총괄하는 함수
    {
        if (GlobalVariable.boardGameUIBool == true) //보드게임 UI가 켜져있는 상태라면
        {
            if (BoardGameUI.activeSelf == false)//꺼져있으면
            {
                BoardGameUI.SetActive(true);
            }

            if (time < 0.5f)
            {
                BoardGameUI.GetComponent<Image>().color = new Color(1, 1, 1, time / 0.5f); //0.5초에 걸쳐 밝아짐
            }
            time += Time.deltaTime;
        }

        if (GlobalVariable.cookUIBool == true) //보드게임 UI가 켜져있는 상태라면
        {
            if (CookUI.activeSelf == false)//꺼져있으면
            {
                CookUI.SetActive(true);
            }
              
            if (time < 0.5f)
            {
                CookUI.GetComponent<Image>().color = new Color(1, 1, 1, time / 0.5f); //0.5초에 걸쳐 밝아짐
            }
            time += Time.deltaTime;
        }

    }


    void BoardGameUIController()
    {
        //레지스탕스 구매여부에 따라 버튼의 정보와 색깔이 달라짐
        if (InventoryInfo.resistance == false)
        {
            ResistanceBuyText.text = "구매";
            ResistanceButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/버튼");
            ResistanceImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UnveiledBoardGame");
        }
        else if (InventoryInfo.resistance == true)
        {
            ResistanceBuyText.text = "구매됨";
            ResistanceButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/버튼_흑백");
            ResistanceImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/레지스탕스");
        }
        //타뷸라 구매여부에 따라 버튼의 정보와 색깔이 달라짐
        if (InventoryInfo.tabula == false)
        {
            TabulaBuyText.text = "구매";
            TabulaButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/버튼");
            TabulaImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UnveiledBoardGame");
        }
        else if (InventoryInfo.tabula == true)
        {
            TabulaBuyText.text = "구매됨";
            TabulaButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/버튼_흑백");
            TabulaImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/타뷸라");
        }
        //붐 구매여부에 따라 버튼의 정보와 색깔이 달라짐
        if (InventoryInfo.boom == false)
        {
            BoomBuyText.text = "구매";
            BoomButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/버튼");
            BoomImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UnveiledBoardGame");
        }
        else if (InventoryInfo.boom == true)
        {
            BoomBuyText.text = "구매됨";
            BoomButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/버튼_흑백");
            BoomImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/붐");
        }
    }

    void CookUIController() //요리 팝업 UI에 사용한 함수입니다.
    {
        
        turkeyText.text = InventoryInfo.turkey.ToString();
        yoggText.text = InventoryInfo.yogg.ToString();
        mushroomText.text = InventoryInfo.mushroom.ToString();
        berryText.text = InventoryInfo.berry.ToString();
        dragonText.text = InventoryInfo.dragon.ToString();
        breadText.text = InventoryInfo.bread.ToString();
        bananaText.text = InventoryInfo.banana.ToString();
    
        //Left

        if(CookItemZoneLeft==CookItemType.nothing)
        {
            CookLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/VoidImage");
        }
        else if (CookItemZoneLeft == CookItemType.turkey)
        {
            CookLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/칠면조");
        }
        else if (CookItemZoneLeft == CookItemType.yogg)
        {
            CookLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/요그소토스의눈알");
        }
        else if (CookItemZoneLeft == CookItemType.mushroom)
        {
            CookLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/발광버섯");
        }
        else if (CookItemZoneLeft == CookItemType.berry)
        {
            CookLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/산딸기");
        }
        else if (CookItemZoneLeft == CookItemType.dragon)
        {
            CookLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/요정드래곤의날개");
        }
        else if (CookItemZoneLeft == CookItemType.bread)
        {
            CookLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/마법식빵");
        }
        else if (CookItemZoneLeft == CookItemType.banana)
        {
            CookLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/보랏빛바나나");
        }

        //Right

        if (CookItemZoneRight == CookItemType.nothing)
        {
            CookRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/VoidImage");
        }
        else if (CookItemZoneRight == CookItemType.turkey)
        {
            CookRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/칠면조");
        }
        else if (CookItemZoneRight == CookItemType.yogg)
        {
            CookRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/요그소토스의눈알");
        }
        else if (CookItemZoneRight == CookItemType.mushroom)
        {
            CookRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/발광버섯");
        }
        else if (CookItemZoneRight == CookItemType.berry)
        {
            CookRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/산딸기");
        }
        else if (CookItemZoneRight == CookItemType.dragon)
        {
            CookRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/요정드래곤의날개");
        }
        else if (CookItemZoneRight == CookItemType.bread)
        {
            CookRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/마법식빵");
        }
        else if (CookItemZoneRight == CookItemType.banana)
        {
            CookRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Item/보랏빛바나나");
        }


    }

    void CookingController() //요리 도중에 사용하는 함수입니다.
    {
        

        //cookingTime += Time.deltaTime;


    }

    //아래는 버튼에 사용한 스크립트입니다.

    public void ResistanceItemBuy() //아이템 구매 
    {
        if (InventoryInfo.resistance == false)
        {
            InventoryInfo.money -= 12000;
            InventoryInfoScript.MoneyGainEffect(-12000);
            InventoryInfo.resistance = true;
        }       
    }

    public void TabulaItemBuy() //아이템 구매 
    {
        if (InventoryInfo.tabula == false)
        {
            InventoryInfo.money -= 23000;
            InventoryInfoScript.MoneyGainEffect(-23000);
            InventoryInfo.tabula = true;
        }
    }

    public void BoomItemBuy() //아이템 구매 
    {
        if (InventoryInfo.boom == false)
        {
            InventoryInfo.money -= 47000;
            InventoryInfoScript.MoneyGainEffect(-47000);
            InventoryInfo.boom = true;
        }
    }

    public void CookButton()
    {
        //둘중에 하나가 비어 있는 경우에는 요리가 되지 않게 해야함.
        if (CookItemZoneLeft == CookItemType.nothing || CookItemZoneRight == CookItemType.nothing) return;

        CookingState = CookingPotState.isCooking;
        Debug.Log("요리...시작합니다...uicontroller");
        CookExitButton();
    }

    public void BoardGameExitButton() // 보드게임 UI X버튼을 누른다면?
    {
        GlobalVariable.boardGameUIBool = false; //UI 꺼져있는지 확인하는 불 을 false로
        BoardGameUI.SetActive(false); //UI도 끄기
        time = 0; //시간도 초기화
    }

    public void CookExitButton() // 쿡 UI X버튼을 누른다면?
    {
        GlobalVariable.cookUIBool = false; //UI 꺼져있는지 확인하는 불 을 false로
        CookUI.SetActive(false); //UI도 끄기
        time = 0; //시간도 초기화
    }

}
