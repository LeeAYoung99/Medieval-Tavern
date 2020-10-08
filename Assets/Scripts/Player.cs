using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 이 스크립트는 플레이어의 클릭 이동과 애니메이션 그리고 물체가 있는 범위에 충돌하면 어떻게 되는지 담고 있습니다.
    /// </summary>

    private Transform _transform;
    private NavMeshAgent nvAgent;
    private Camera cam;
    private Vector3 targetPos; // 캐릭터의 이동 타겟 위치
    private float distanceLeft;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponent<Animator>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        targetPos = this.gameObject.transform.position;
        
        distanceLeft = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }
    
    void MoveCharacter()
    {
        if (GlobalVariable.isUIOn()) return; //UI가 켜져있으면 안움직임

        // 마우스 입력을 받았 을 때
        if (Input.GetMouseButtonUp(0))
        {
            // 마우스로 찍은 위치의 좌표 값을 가져온다
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f))
            {

                if (hit.transform.name == "Floor") //땅을 클릭하면
                {
                    targetPos = hit.point;
                    // 추적 대상의 위치를 설정하면 바로 추적 시작
                    nvAgent.destination = targetPos;
                }

            }
        }
        distanceLeft = Vector3.Distance(targetPos, this.gameObject.transform.position); //거리 계산
        UpdateAnimation(distanceLeft); //거리에 맞추어 애니메이션 업데이트 

        return;

    }

    void UpdateAnimation(float _distanceLeft) //distanceLeft는 도착 지점과 현제 플레이어 사이의 거리!
    {
        if(_distanceLeft>1.0f) //도착지점 거리차이에 비례해서 애니메이터가 작동하고 안하게 함
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "BoardGame")//보드게임 영역에 충돌하면
        {
            targetPos = this.gameObject.transform.position; //제자리로 가!
            nvAgent.destination = targetPos;
            UpdateAnimation(0);//그 자리에 멈추니까 거리를 0으로 주는 애니메이션 업뎃
            
            GlobalVariable.boardGameUIBool = true; //보드게임 UI가 켜졌다!
        }
        else if (col.transform.tag == "Cook") // Cook 영역에 충돌하면
        {
            targetPos = this.gameObject.transform.position; //제자리로 가!
            nvAgent.destination = targetPos;
            UpdateAnimation(0);//그 자리에 멈추니까 거리를 0으로 주는 애니메이션 업뎃

            if (UIController.CookingState == UIController.CookingPotState.isPotEmpty)
            {
                GlobalVariable.cookUIBool = true; //쿠킹 UI가 켜졌다!
            }
            else if (UIController.CookingState == UIController.CookingPotState.isCooking)
            {
                //쿠킹 게이지 올라가는 거 보여주기
            }
            else if (UIController.CookingState == UIController.CookingPotState.isFoodReady)
            {
                //완료된 음식 보여주기
            }
        }

    }
    
}