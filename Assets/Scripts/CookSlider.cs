using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookSlider : MonoBehaviour
{
    public GameObject Cook;
    public GameObject MyCamera;
    public GameObject SliderBackground;
    public GameObject SliderFill;

    public float minimumDist = 23.0f;
    public float maximumDist = 60.0f;

    float camToCookDistance;

    // Start is called before the first frame update
    void Start()
    {
        camToCookDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSliderTransparent();
        // 오브젝트에 따른 HP Bar 위치 이동
        this.transform.position = Camera.main.WorldToScreenPoint(Cook.transform.position + new Vector3(3.0f, 0.8f, 2.0f));
    }

    void ChangeSliderTransparent()
    {
        //거리에 비례해서 슬라이더 투명도 다르게 하기
        camToCookDistance = Vector3.Distance(MyCamera.gameObject.transform.position, Cook.gameObject.transform.position);//거리 재기
        if (minimumDist < camToCookDistance && camToCookDistance < maximumDist)
        {
            SliderFill.GetComponent<Image>().color = new Color(1, 1, 1, 1.0f - (camToCookDistance - minimumDist) / (maximumDist - minimumDist));
            SliderBackground.GetComponent<Image>().color = new Color(1, 1, 1, 1.0f - (camToCookDistance - minimumDist) / (maximumDist - minimumDist));
        }
        else if (camToCookDistance <= 23)
        {
            SliderFill.GetComponent<Image>().color = new Color(1, 1, 1, 1.0f);
            SliderBackground.GetComponent<Image>().color = new Color(1, 1, 1, 1.0f);
        }
        else
        {
            SliderFill.GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);
            SliderBackground.GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);
        }
    }
}
