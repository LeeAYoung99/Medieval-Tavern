using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    private Camera cam;
    public GameObject ClickPrefab;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(this.ClickEventOccur());
    }

    IEnumerator ClickEventOccur()
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
                        Instantiate(ClickPrefab, new Vector3(hit.point.x, hit.point.y+0.2f, hit.point.z) , Quaternion.identity);
                    }

                }
            }
            
            

        }

        yield return null;

    }
}
