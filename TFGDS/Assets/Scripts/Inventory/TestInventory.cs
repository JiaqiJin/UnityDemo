using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour
{
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("press o");
            int id = Random.Range(1,2);
            Knapsack.Instance.StoreItem(id);
        } 
    }
}
