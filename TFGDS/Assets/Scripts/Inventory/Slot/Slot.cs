using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject itemPrefabs;

    /// <summary>
    /// meter objetos de slots
    /// si hay objetos amout ++;
    /// sino coge un objeto del slot de prefabs
    /// </summary>
    /// <param name="item"></param>
    public void StoreItem(Item item)
    {
        if(transform.childCount == 0)
        {
           GameObject itemGameObject = Instantiate(itemPrefabs) as GameObject;
            itemGameObject.transform.SetParent(this.transform);
            itemGameObject.transform.localPosition = Vector3.zero;
            itemGameObject.GetComponent<ItemUI>().SetItem(item);
        }

        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount();
        }
    }


    /// <summary>
    /// Obtener el objeto actual del mismo tipo
    /// </summary>
    /// <returns></returns>
    public ItemType GetItemType()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ItemType;

    }

    public bool IsFilled()
    {
        ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
        return itemUI.Amount >= itemUI.Item.Capacity;
    }

}
