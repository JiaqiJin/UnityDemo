using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            //Debug.Log("press o");
            int id = Random.Range(1,19);
            //int id = Random.Range(1, 3);
            Knapsack.Instance.StoreItem(id);
        }
        // ocultar panel
        if (Input.GetKeyDown(KeyCode.T))
        {
            Knapsack.Instance.DisplaySwitch();
            Chest.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CharacterPanel.Instance.DisplaySwitch();
        }
    }
}
