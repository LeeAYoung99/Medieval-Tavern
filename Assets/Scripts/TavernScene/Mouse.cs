using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mouse : MonoBehaviour
{
    private NavMeshAgent nvAgent;
    private Vector3 targetPos; // 캐릭터의 이동 타겟 위치
    private Animator animator;
    private GameObject PlayerLocation;//플레이어 위치만 가져올거임!
    private GameObject DoorLocation;

    public GameObject TalkBox;

    GameObject Canvas;
    public GameObject BuffUI;
    

    bool wait = false;//wait함수에서 쓸
    bool beforewait = true;
    public static bool isTalking = false;//UI카메라움직임 확인
    

    bool isChanged = false; //상태가 막 바뀐 때에서만 코드가 발생하도록 ㅠㅠ
    
    GameObject ownedUI;
    
    Vector3 startPos;//시작 포지션

    int chairIndex;
    int orderCount;

    private InventoryInfo InventoryInfoScript;

    float mytime = 0;

    public enum MouseState
    {
        FindDoorIn = 0,
        WaitPlayer,
        Talk, //뀨
        FindDoorOut,
        GoOut
    }
    MouseState myState = 0;

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

        Canvas = GameObject.Find("BuffOnCanvas");


    }

    void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (myState)
        {
            case MouseState.FindDoorIn:
                FindDoorIn();
                return;

            case MouseState.WaitPlayer:
                WaitPlayer();
                return;

            case MouseState.Talk:
                Talk();
                return;

            case MouseState.FindDoorOut:
                FindDoorOut();
                return;

            case MouseState.GoOut:
                GoOut();
                return;
        }

  
    }

    void FindDoorIn()
    {
        if (targetPos == new Vector3(0, 0, 0)) //타겟 포지션이 정해지지 않았을때
        {
            targetPos = new Vector3(DoorLocation.transform.position.x, DoorLocation.transform.position.y, DoorLocation.transform.position.z+2.0f);
        }

        nvAgent.destination = targetPos;

        if (Vector3.Distance(targetPos, transform.position) < 1.6f)
        {
            isChanged = true;
            myState = MouseState.WaitPlayer;
        }

    }

    void WaitPlayer()
    {
        
        if (isChanged)
        {
            isChanged = false;
        }
        ////
        ///


        if (Vector3.Distance(transform.position, PlayerLocation.transform.position) < 2.0f) //뀨
        {
            myState = MouseState.Talk;
            isChanged = true;
        }



        /////

        if (Vector3.Distance(targetPos, transform.position) < 1.0f)
        {

            if (!wait)
            {
                wait = true;
            }
            else
            {
                wait = false;
            }

        }


        if (beforewait!=wait)
        {
            targetPos = new Vector3(Random.Range(20.56f, 25.52f),transform.position.y, Random.Range(-28.81f, -34.5f));
            nvAgent.destination = targetPos;
        }
        

        if (mytime < 40.0f)
        {
            mytime += Time.deltaTime;
        }

        if (mytime >= 40.0f)
        {
            myState = MouseState.FindDoorOut;
            isChanged = true;
        }

        beforewait = wait;
    }

    void Talk()
    {
        if(isChanged)
        {
            isChanged = false;
        }
        nvAgent.destination = transform.position;

        TalkBox.SetActive(true);
        
    }
  
    void FindDoorOut()
    {
        if (isChanged)
        {
           // animator.SetInteger("State", 0);
            nvAgent.enabled = true;
            targetPos = DoorLocation.transform.position;
            nvAgent.destination = targetPos;

            isChanged = false;
        }
        if (Vector3.Distance(targetPos, transform.position) < 1.6f)
        {
            isChanged = true;
            myState = MouseState.GoOut;
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

    //버튼함수

    public void PressOk()
    {
        GameObject _buff;

        TalkBox.SetActive(false);
        _buff = Instantiate(BuffUI, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        _buff.gameObject.transform.SetParent(Canvas.transform, false);

        isChanged = true;
        myState = MouseState.FindDoorOut;
    }

}
