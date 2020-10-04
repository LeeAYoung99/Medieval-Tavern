using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEventPrefab : MonoBehaviour
{
    //ClickEvent 스크립트에서 Instantiate 동적생성됩니다.

   
    float EffectTime;
    float _FadeTime = 0.5f;
    float speed = 0.005f;


    // Start is called before the first frame update
    void Start()
    {
        EffectTime = 0;
      
    }

    // Update is called once per frame
    void Update()
    {
        EffectUpdate();
    }

    void EffectUpdate()
    {

        if (EffectTime < _FadeTime)
        {
            transform.localScale = new Vector3(transform.localScale.x + EffectTime * speed, transform.localScale.y, transform.localScale.z + EffectTime * speed);

        }
        else
        {
            Destroy(this.gameObject);

        }
        EffectTime += Time.deltaTime;
    }
}
