using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    public static float energy = 1.0f;
    private Slider MySlider;
    PlayerBroom playerbroom;
    float mytime = 0;

    // Start is called before the first frame update
    void Start()
    {
        MySlider = this.gameObject.GetComponent<Slider>();
        playerbroom = GameObject.Find("BroomSample").GetComponent<PlayerBroom>();
    }

    // Update is called once per frame
    void Update()
    {
        MySlider.value = energy;

        if(GlobalVariable.isBuffOn)
        {
            return;
        }

        if (!Player.isMoving)
        {
            if (mytime < 3.0f)
            {
                mytime += Time.deltaTime;
            }
            if (mytime >= 2.9f)
            {
                if (energy + 0.1f * Time.deltaTime < 1)
                    energy += 0.1f * Time.deltaTime;
            }


        }
        else if (playerbroom.broomOwn)
        {
            mytime = 0;
            if (energy - 0.08f * Time.deltaTime > 0)
                energy -= 0.08f * Time.deltaTime;
        }
        else if(Player.isMoving)
        {
            mytime = 0;
            if (energy - 0.03f * Time.deltaTime > 0)
                energy -= 0.03f * Time.deltaTime;
        }
      
    
    }
}
