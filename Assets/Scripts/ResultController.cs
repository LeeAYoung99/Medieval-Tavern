using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour
{
    public int StartMoney;
    public int EndMoney;
    public int StartRep;
    public int EndRep;
    

    // Start is called before the first frame update
    void Start()
    {
        StartMoney = InventoryInfo.money;
        StartRep = InventoryInfo.reputation;
        EndMoney = 0;
        EndRep = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
       
    }
}
