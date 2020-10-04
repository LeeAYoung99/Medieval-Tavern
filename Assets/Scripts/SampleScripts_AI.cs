using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SampleScripts_AI : MonoBehaviour
{

    private Transform _transform;
   // private Transform playerTransform;
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
        StartCoroutine(this.CharacterMove());
        
    }
    
    IEnumerator CharacterMove()
    {
        if (!GlobalVariable.isUIOn()) //UI가 꺼져있다면
        {
            // 마우스 입력을 받았 을 때
            if (Input.GetMouseButtonUp(0))
            {
                // 마우스로 찍은 위치의 좌표 값을 가져온다
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10000f))
                {

                    if (hit.transform.name == "Floor")
                    {
                        targetPos = hit.point;
                    }

                }
            }
            distanceLeft = Vector3.Distance(targetPos, this.gameObject.transform.position);
            AnimationUpdate(distanceLeft);

            // 추적 대상의 위치를 설정하면 바로 추적 시작
            nvAgent.destination = targetPos;


        }

        yield return null;

    }

    void AnimationUpdate(float _distanceLeft)
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
        if (col.transform.tag == "BoardGame")
        {
            nvAgent.destination = this.gameObject.transform.position;
            
            GlobalVariable.boardGameUIBool = true;
        }
        if (col.transform.tag == "Cook")
        {
            nvAgent.destination = this.gameObject.transform.position;
            
            GlobalVariable.cookUIBool = true;
        }

    }
    
}