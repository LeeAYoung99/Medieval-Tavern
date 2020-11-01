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

    public Transform GuestOwnedParent;
    public GameObject GuestOwnedPrefab;

    bool isChanged = false; //상태가 막 바뀐 때에서만 코드가 발생하도록 ㅠㅠ

    int rand = 0; //랜덤한 번째의 의자

    public static UIController.Food guestOrderedFood;
    GameObject ownedUI;

    float time = 0;
    Vector3 startPos;//시작 포지션
    Vector3 sitBeforePos; //앉기 직전 포지션

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
        FindChair = 0,
        Order = 1,
        SitAndEat = 2,
        GoOut = 3
    }
    NormalGuestState myState = 0;

    // Start is called before the first frame update
    void Start()
    {
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        targetPos = new Vector3(0, 0, 0);
        animator = this.gameObject.GetComponent<Animator>();
        PlayerLocation = GameObject.Find("Player");
        startPos = transform.position;

        InventoryInfoScript = GameObject.Find("InventoryInfo").GetComponent("InventoryInfo") as InventoryInfo;

        guestOrderedFood = (UIController.Food)Random.Range(2, System.Enum.GetValues(typeof(UIController.Food)).Length);
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (myState)
        {
            case NormalGuestState.FindChair:
                FindChair();
                return;

            case NormalGuestState.Order:
                Order();
                return;

            case NormalGuestState.SitAndEat:
                SitAndEat();
                return;

            case NormalGuestState.GoOut:
                GoOut();
                return;
        }
    }

    void FindChair()
    {
        if (targetPos == new Vector3(0,0,0)) //타겟 포지션이 정해지지 않았을때
        {
            rand = Random.Range(0, 16); //랜덤 의자 뽑기
            targetPos = new Vector3(ChairInfo.chairs[rand].pos.x, transform.position.y, ChairInfo.chairs[rand].pos.z - 3.0f); //그 의자 위치로 이동
        }
        
        nvAgent.destination = targetPos;

        if (Vector3.Distance(targetPos, transform.position) < 0.6f)
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
            nvAgent.enabled = false;
            transform.position = new Vector3(ChairInfo.chairs[rand].pos.x, ChairInfo.chairs[rand].pos.y + 0.85f, ChairInfo.chairs[rand].pos.z);
            transform.rotation = ChairInfo.chairs[rand].rot;
            transform.position += transform.forward * 0.8f;
            animator.SetInteger("State", 1);
            ownedUI = Instantiate(GuestOwnedPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            ownedUI.gameObject.transform.SetParent(GuestOwnedParent, false);

            isChanged = false;
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
            animator.SetInteger("State", 2);

            isChanged = false;
        }
        time += Time.deltaTime;
        if (time > 10.0f)
        {
            isChanged = true;
            myState = NormalGuestState.GoOut;
        }
    }
    void GoOut()
    {
        if (isChanged)
        {
            transform.position = sitBeforePos;
            animator.SetInteger("State", 0);
            nvAgent.enabled = true;
            targetPos = startPos;
            nvAgent.destination = targetPos;

            isChanged = false;
        }
        if (Vector3.Distance(transform.position, startPos) < 2.8f)
        {
            Destroy(this.gameObject);
        }
    }
    
}
