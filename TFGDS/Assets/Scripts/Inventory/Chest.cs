using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Inventory
{
    private static Chest instance_;
    public static Chest Instance
    {
        get
        {
            if (instance_ == null)
            {
                //Debug.Log()
                instance_ = GameObject.Find("ChestPanel").GetComponent<Chest>();

            }
            return instance_;
        }
    }
}
