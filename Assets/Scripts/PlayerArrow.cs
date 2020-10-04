using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    Vector3 pos; //현재위치
    Vector3 playerPos; //플레이어 좌표
    Vector3 posDiff; //초기 좌표차이
    float delta = 0.5f; // 상하로 이동가능한 (y)최대값
    float speed = 6.0f; // 이동속도
    //float rotatespeed = 100.0f; //자전속도

    public GameObject Player;

    void Start()
    {
        pos = transform.position;
        playerPos = Player.transform.position;
        posDiff = pos - playerPos;
    }


    void Update()
    {

        playerPos = Player.transform.position;

        Vector3 v = pos;
        v.y += delta * Mathf.Sin(Time.time * speed);

        //상하 이동의 최대치 및 반전 처리

        this.gameObject.transform.position = new Vector3(playerPos.x + posDiff.x, playerPos.y + v.y, playerPos.z + posDiff.z);

    }
}
