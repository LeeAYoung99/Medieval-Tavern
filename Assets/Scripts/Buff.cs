using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{

    public float buffTime = 1.0f;


    Image buffImage;

    // Start is called before the first frame update
    void Start()
    {
        buffImage = gameObject.GetComponent<Image>();
        StartCoroutine("BuffCount");
        PlayerEnergy.energy = 1.0f;
        GlobalVariable.isBuffOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        buffImage.fillAmount = buffTime;
        if(buffTime <= 0)
        {
            GlobalVariable.isBuffOn = false;
            Destroy(transform.parent.gameObject);
        }
    }

    IEnumerator BuffCount()
    {
        while (buffTime > 0)
        {
            yield return new WaitForSeconds(0.05f);
            buffTime -= 0.001f;
        }
        
    }

}
