using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knapsack : Inventory
{
    private static Knapsack instance_;
    public static Knapsack Instance
    {
        get
        {
            if(instance_ == null)
            {
                //Debug.Log()
                instance_ = GameObject.Find("KnapsackPanel").GetComponent<Knapsack>();
              
            }
            return instance_;
        }
    }
}
