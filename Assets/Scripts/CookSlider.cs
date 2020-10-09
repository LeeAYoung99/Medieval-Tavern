using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookSlider : MonoBehaviour
{
    private GameObject Cook;
    private GameObject MyCamera;
    public GameObject SliderBackground;
    public GameObject SliderFill;

    private Slider MySlider;

    float cookTime;
    float maxTime = 25.0f;

    public float minimumDist = 23.0f;
    public float maximumDist = 80.0f;

    float camToCookDistance;

    // Start is called before the first frame update
    void Start()
    {
        Cook = GameObject.Find("FoodKit");
        MyCamera = GameObject.Find("Main Camera");
        MySlider = this.gameObject.GetComponent<Slider>();
        cookTime = 0;
        camToCookDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(cookTime>maxTime)
        {
            UIController.CookingState = UIController.CookingPotState.isFoodReady;
            Destroy(this.gameObject);
            return;
        }

        ChangeSliderTransparent();
        // 오브젝트에 따른 HP Bar 위치 이동
        this.transform.position = Camera.main.WorldToScreenPoint(Cook.transform.position + new Vector3(3.0f, 0.8f, 2.0f));
        cookTime += Time.deltaTime;
        MySlider.value = cookTime / maxTime;
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
