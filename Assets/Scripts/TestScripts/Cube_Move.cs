using UnityEngine;
using System.Collections;

public class Cube_Move : MonoBehaviour {
    
    float time = 0f;

	void Update () {

        time += Time.deltaTime;

        // 크기 시간비례해서 줄어들기
        transform.localScale = new Vector3(1.0f - time, 1.0f - time, 1.0f - time) * 50.0f;

        if (time > 0.7f)
        {
            gameObject.SetActive(false);
            time = 0;
        }



    }
	

}
