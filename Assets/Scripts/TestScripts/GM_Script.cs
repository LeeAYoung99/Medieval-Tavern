using UnityEngine;
using System.Collections;

public class GM_Script : MonoBehaviour {

	private Vector3 StartPos_Vec;
	public GameObject StartPos;

	void Start () {
		
		
	}

    float time;

    void Update()
    {
        StartPos_Vec = StartPos.transform.position; //구름 시작점

        time += Time.deltaTime;
        if (time > 0.1f)
        {
            if(Player.isMoving)
            {
                CUBE();
            }
            
            time = 0;
        }
    }

	void CUBE(){

		GameObject obj = NewObjPooling.current.GetPooledObject(); 
		
		if(obj == null) return;
		
		obj.transform.position = StartPos_Vec; 
		obj.SetActive(true);
	}

}
