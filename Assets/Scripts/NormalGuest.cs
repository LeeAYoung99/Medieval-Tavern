using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalGuest : MonoBehaviour
{
    private NavMeshAgent nvAgent;
    private Vector3 targetPos; // 캐릭터의 이동 타겟 위치
    private Animator animator;
    private GameObject PlayerLocation;//플레이어 위치만 가져올거임!
    private GameObject DoorLocation;

    public Transform GuestOwnedParent;
    public GameObject GuestOwnedPrefab;

    bool isChanged = false; //상태가 막 바뀐 때에서만 코드가 발생하도록 ㅠㅠ
    bool wasOrdered = false;

    int chairNum = 0; //랜덤한 번째의 의자

    public UIController.Food guestOrderedFood; //주문할 음식
    GameObject ownedUI;

    float time = 0;
    Vector3 startPos;//시작 포지션
    Vector3 sitBeforePos; //앉기 직전 포지션

    int chairIndex;
    int orderCount;

    private InventoryInfo InventoryInfoScript;

    Dictionary<UIController.Food, int> foodSellPrice = new Dictionary<UIController.Food, int>()
    {
        {UIController.Food.Nothing, 0},
        {UIController.Food.Spoiled, 0},
        {UIController.Food.WitchSoup, 50},
        {UIController.Food.BerrySandwich, 80},
        {UIController.Food.WingSalad, 90},
        {UIController.Food.RoastedTurkey, 100},
        {UIController.Food.Stick, 180},
        {UIController.Food.Beer, 30},
        {UIController.Food.SlimeCocktail, 70},
        {UIController.Food.Wine, 65}
    };

    public enum NormalGuestState
    {
        FindDoorIn = 0,
        FindChair,
        Order,
        SitAndEat,
        FindDoorOut,
        GoOut
    }
    NormalGuestState myState = 0;

    // Start is called before the first frame update
    void Start()
    {
        GlobalVariable.currentGuestNum++;

        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        targetPos = new Vector3(0, 0, 0);
        animator = this.gameObject.GetComponent<Animator>();
        PlayerLocation = GameObject.Find("Player");
        DoorLocation = GameObject.Find("Door");
        startPos = transform.position;

        InventoryInfoScript = GameObject.Find("InventoryInfo").GetComponent("InventoryInfo") as InventoryInfo;

        guestOrderedFood = (UIController.Food)Random.Range(2, System.Enum.GetValues(typeof(UIController.Food)).Length);
        orderCount = Random.Range(1, 4);

        chairIndex = GuestGenerator.currentNum; //랜덤 의자 뽑기
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (myState)
        {
            case NormalGuestState.FindDoorIn:
                FindDoorIn();
                return;

            case NormalGuestState.FindChair:
                FindChair();
                return;

            case NormalGuestState.Order:
                Order();
                return;

            case NormalGuestState.SitAndEat:
                SitAndEat();
                return;

            case NormalGuestState.FindDoorOut:
                FindDoorOut();
                return;

            case NormalGuestState.GoOut:
                GoOut();
                return;
        }
    }

    void FindDoorIn()
    {
        if (targetPos == new Vector3(0, 0, 0)) //타겟 포지션이 정해지지 않았을때
        {
            targetPos = DoorLocation.transform.position;
        }

        nvAgent.destination = targetPos;

        if (Vector3.Distance(targetPos, transform.position) < 1.6f)
        {
            isChanged = true;
            myState = NormalGuestState.FindChair;
        }

    }

    void FindChair()
    {
        
        if (isChanged)
        {
            chairNum = chairIndex;
            targetPos = new Vector3(ChairInfo.chairs[chairNum].pos.x, transform.position.y, ChairInfo.chairs[chairNum].pos.z); //그 의자 위치로 이동
            isChanged = false;
        }
        
        nvAgent.destination = targetPos;

        if (Vector3.Distance(targetPos, transform.position) < 1.6f)
        {
            sitBeforePos = transform.position;
            isChanged = true;
           
            myState = NormalGuestState.Order;
        }
    }
    void Order()
    {
        if (isChanged)
        {
            orderCount--;
            nvAgent.enabled = false;

            transform.position = new Vector3(ChairInfo.chairs[chairNum].pos.x, ChairInfo.chairs[chairNum].pos.y + 1.3f, ChairInfo.chairs[chairNum].pos.z);
            transform.rotation = ChairInfo.chairs[chairNum].rot;

            

            if (!wasOrdered)
            {
                transform.position += transform.forward * 0.8f;
                wasOrdered = false;
            }

            animator.SetInteger("State", 1);
            ownedUI = Instantiate(GuestOwnedPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            ownedUI.gameObject.transform.SetParent(GuestOwnedParent, false);

            isChanged = false;
            time = 0;
        }

        //시간이 다 되면 주문 생까고 나가기
        time += Time.deltaTime;
        if (time > 40.0f)
        {
            isChanged = true;
            if (ownedUI)
            {
                Destroy(ownedUI.gameObject);
            }
            animator.SetInteger("State", 2);
            myState = NormalGuestState.FindDoorOut;
        }

        if (Vector3.Distance(transform.position, PlayerLocation.transform.position) < 2.8f && guestOrderedFood == Player.playerOwnedFood) //음식 전달. 숫자 조절하면 거리 달라짐
        {
            foreach (KeyValuePair<UIController.Food, int> items in foodSellPrice) //음식 가격에 맞추어 돈벌기
            {
                if (guestOrderedFood == items.Key)
                {
                    InventoryInfo.money += items.Value;
                    InventoryInfoScript.MoneyGainEffect(items.Value);
                }
            }
            
            Player.playerOwnedFood = UIController.Food.Nothing;
            myState = NormalGuestState.SitAndEat;
            if (ownedUI)
            {
                Destroy(ownedUI.gameObject);
            }
            isChanged = true;

        }
    }
    void SitAndEat()
    {
        if(isChanged)
        {
            ChairInfo.chairs[chairNum].isAllocated = true; //자리있어요
            animator.SetInteger("State", 2);

            isChanged = false;
            time = 0;
        }
        time += Time.deltaTime;
        if (time > 10.0f)
        {
            isChanged = true;
            if (orderCount <= 0)
            {
                myState = NormalGuestState.FindDoorOut;
                return;
            }
            myState = NormalGuestState.Order;
            wasOrdered = true;
            guestOrderedFood = (UIController.Food)Random.Range(2, System.Enum.GetValues(typeof(UIController.Food)).Length);
            orderCount--;

        }
    }
    void FindDoorOut()
    {
        if (isChanged)
        {

            ChairInfo.chairs[chairNum].isAllocated = false; //자리없어용

            transform.position = sitBeforePos;
            animator.SetInteger("State", 0);
            nvAgent.enabled = true;
            targetPos = DoorLocation.transform.position;
            nvAgent.destination = targetPos;

            isChanged = false;
        }
        if (Vector3.Distance(targetPos, transform.position) < 1.6f)
        {
            isChanged = true;
            myState = NormalGuestState.GoOut;
        }
    }
    void GoOut()
    {
        if (isChanged)
        {
            targetPos = startPos;
            nvAgent.destination = targetPos;

            isChanged = false;
        }
        if (Vector3.Distance(transform.position, startPos) < 2.8f)
        {
            GlobalVariable.currentGuestNum--;
            Destroy(this.gameObject);
        }
    }
    
}
