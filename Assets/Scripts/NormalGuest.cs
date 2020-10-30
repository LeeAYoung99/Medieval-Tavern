using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalGuest : MonoBehaviour
{
    private Transform _transform;
    private NavMeshAgent nvAgent;
    private Vector3 targetPos; // 캐릭터의 이동 타겟 위치
    private Animator animator;

    public Transform GuestOwnedParent;
    public GameObject GuestOwnedPrefab;

    bool isChanged = false; //상태가 막 바뀐 때에서만 코드가 발생하도록 ㅠㅠ

    int rand = 0; //랜덤한 번째의 의자

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
        _transform = this.gameObject.GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        targetPos = new Vector3(0, 0, 0);
        animator = this.gameObject.GetComponent<Animator>();
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
            animator.SetBool("isSitting", true);
            GameObject ownedUI = Instantiate(GuestOwnedPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            ownedUI.gameObject.transform.SetParent(GuestOwnedParent, false);
            isChanged = false;
        }
     
    }
    void SitAndEat()
    {

    }
    void GoOut()
    {

    }
    
}
