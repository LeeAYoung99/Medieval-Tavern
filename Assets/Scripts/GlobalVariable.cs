using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariable : MonoBehaviour
{
    public static bool boardGameUIBool = false;
    public static bool cookUIBool = false;
    public static bool cookSliderBool = false;
    public static bool didYouClickedButton = false;

    public static bool isUIOn() //UI가 한개라도 켜져있는지 리턴하는 전역함수.
    {
        if (boardGameUIBool == true)
        {
            return true;
        }
        if (cookUIBool == true)
        {
            return true;
        }

        return false;
    }
    
}
