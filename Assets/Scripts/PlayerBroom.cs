using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBroom : MonoBehaviour
{
    public GameObject Broom;
    GameObject _Player;
    public bool broomOwn = false;
    bool flip = false;
    float mytime = 0;

    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        mytime += Time.deltaTime;
        if (mytime > 0.4f)
        {
            if (flip == true) flip = false;
            else if (flip == false) flip = true;
            mytime = 0;
        }

        if (flip == true && broomOwn == true)
        {
            Broom.transform.Rotate(Vector3.up * Time.deltaTime * 40.0f);
        }
        else if (flip == false && broomOwn == true)
        {
            Broom.transform.Rotate(Vector3.down * Time.deltaTime * 40.0f);
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && broomOwn == false)
        {
            if (Player.playerOwnedFood != UIController.Food.Nothing)
                return;

            Broom.transform.SetParent(_Player.transform);
            Broom.transform.localPosition = new Vector3(0, 0.8f, 1.0f);
           // Broom.transform.localRotation = new Quaternion(0, 0, 0, 0);
            broomOwn = true;
        }
        else if (col.tag == "Player" && broomOwn == true)
        {
            Broom.transform.SetParent(this.transform);
            Broom.transform.localPosition = new Vector3(0, 0, 0);
            Broom.transform.localRotation = new Quaternion(0, 0, 0, 0);
            broomOwn = false;
        }
    }
}
