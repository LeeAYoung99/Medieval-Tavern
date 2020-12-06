using UnityEngine;
using System.Collections;
using System.Collections.Generic; // 리스트를 쓰기 위해 추가

public class NewObjPooling : MonoBehaviour {

	public static NewObjPooling current;  //모든 클래스에서 직접호출 가능

    public GameObject CloudObj;

    public GameObject CloudParent; // obj를 Play_Obj 밑으로생성

	public int PoolAmount = 6;

	public List<GameObject> PoolObjs;

	void Awake()
	{
		//static으로 선언한 NewObjPooling current에 접근
		current = this;
	}

	void Start () {
		PoolObjs = new List<GameObject>();

		for(int i = 0; i < PoolAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(CloudObj);

			obj.transform.parent = CloudParent.transform; //[자식] obj_A -> [부모] Play_Obj 밑으로 생성하기

			//PoolObj -> obj에 저장
			obj.SetActive (false);
			PoolObjs.Add(obj);
			// Instantiate로 그려지고 비활성화된 상태의 오브젝트를 PoolObjs에 차곡차곡 넣는다.
		}

	}



	public GameObject GetPooledObject()
	{
		for(int i = 0; i < PoolObjs.Count; i++)
		{

			//obj.SetActive 가 false면 실행 
			if(!PoolObjs[i].activeInHierarchy)
			{
				//GM의 CUBE_A()에서 넘어온 obj.SetActive(true)된 Cube_A 호출
				return PoolObjs[i];
			}
		}
		return null;
		// PoolObjs에 prefab이 남아있지 않으면 null을 넘겨준다.
	}


}
