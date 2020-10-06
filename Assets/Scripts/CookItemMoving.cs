using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookItemMoving : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    /// <summary>
    /// 이 스크립트는 인스턴스화 한 아이템들을 드래그 앤 드랍하기 위해 만들어졌습니다.
    /// </summary>

    public static Vector2 defaultposition;//드롭하면 다시 원위치로 보내기위한 변수
    public Transform Canvas;
    public Transform Parent;
    bool isLeftEnter;
    bool isRightEnter;
    

    // Start is called before the first frame update
    void Start()
    {
        bool isLeftEnter = false;
        bool isRightEnter = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)//드래그시작할 때
    {
        this.gameObject.transform.SetParent(Canvas, true);
        defaultposition = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)//드래그중일 때
    {
        Vector2 currentPos = Input.mousePosition;
        this.transform.position = currentPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)//드래그 끝났을 때 
    {
        if (isLeftEnter == true) //드래그 뗄 때 Left에 오브젝트가 올려져 있다면?
        {
            if (this.gameObject.transform.name == "TurkeyDrag") //그것이 TurkeyDrag 오브젝트라면?
            {
                UIController.CookItemZoneLeft = UIController.CookItemType.turkey;
            }
            else if (this.gameObject.transform.name == "YoggDrag") //그것이 YoggDrag 오브젝트라면?
            {
                UIController.CookItemZoneLeft = UIController.CookItemType.yogg;
            }
            else if (this.gameObject.transform.name == "MushroomDrag") //그것이 MushroomDrag 오브젝트라면?
            {
                UIController.CookItemZoneLeft = UIController.CookItemType.mushroom;
            }
            else if (this.gameObject.transform.name == "BerryDrag") //그것이 BerryDrag 오브젝트라면?
            {
                UIController.CookItemZoneLeft = UIController.CookItemType.berry;
            }
            else if (this.gameObject.transform.name == "DragonDrag") //그것이 DragonDrag 오브젝트라면?
            {
                UIController.CookItemZoneLeft = UIController.CookItemType.dragon;
            }
            else if (this.gameObject.transform.name == "BreadDrag") //그것이 BreadDrag 오브젝트라면?
            {
                UIController.CookItemZoneLeft = UIController.CookItemType.bread;
            }
            else if (this.gameObject.transform.name == "BananaDrag") //그것이 BananaDrag 오브젝트라면?
            {
                UIController.CookItemZoneLeft = UIController.CookItemType.banana;
            }
        }
        if (isRightEnter == true) //드래그 뗄 때 Right에 오브젝트가 올려져 있다면?
        {
            if (this.gameObject.transform.name == "TurkeyDrag") //그것이 TurkeyDrag 오브젝트라면?
            {
                UIController.CookItemZoneRight = UIController.CookItemType.turkey;
            }
            else if (this.gameObject.transform.name == "YoggDrag") //그것이 YoggDrag 오브젝트라면?
            {
                UIController.CookItemZoneRight = UIController.CookItemType.yogg;
            }
            else if (this.gameObject.transform.name == "MushroomDrag") //그것이 MushroomDrag 오브젝트라면?
            {
                UIController.CookItemZoneRight = UIController.CookItemType.mushroom;
            }
            else if (this.gameObject.transform.name == "BerryDrag") //그것이 BerryDrag 오브젝트라면?
            {
                UIController.CookItemZoneRight = UIController.CookItemType.berry;
            }
            else if (this.gameObject.transform.name == "DragonDrag") //그것이 DragonDrag 오브젝트라면?
            {
                UIController.CookItemZoneRight = UIController.CookItemType.dragon;
            }
            else if (this.gameObject.transform.name == "BreadDrag") //그것이 BreadDrag 오브젝트라면?
            {
                UIController.CookItemZoneRight = UIController.CookItemType.bread;
            }
            else if (this.gameObject.transform.name == "BananaDrag") //그것이 BananaDrag 오브젝트라면?
            {
                UIController.CookItemZoneRight = UIController.CookItemType.banana;
            }
        }
        this.gameObject.transform.SetParent(Parent, true);
        transform.position = defaultposition;
       
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "COOKLEFT")
        {
            isLeftEnter = true;
        }
        if (col.gameObject.tag == "COOKRIGHT")
        {
            isRightEnter = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "COOKLEFT")
        {
            isLeftEnter = false;
        }
        if (col.gameObject.tag == "COOKRIGHT")
        {
            isRightEnter = false;
        }
    }
}
