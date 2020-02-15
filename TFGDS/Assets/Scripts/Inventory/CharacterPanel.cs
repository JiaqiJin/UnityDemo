using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : Inventory
{

    private static CharacterPanel instance_;
    public static CharacterPanel Instance
    {
        get
        {
            if (instance_ == null)
            {
                //Debug.Log()
                instance_ = GameObject.Find("CharacterPanel").GetComponent<CharacterPanel>();

            }
            return instance_;
        }
    }


    public void PutOn(Item item)
    {
        Item outItem = null;
        foreach(Slot slot in slotList)
        {
            EquipmentSlot equipmentSlot = (EquipmentSlot)slot; // transforma en qup slot
            if (equipmentSlot.IsRightItem(item))
            {
                if(equipmentSlot.transform.childCount > 0) // ya tiene armaduras
                {
                    ItemUI currentItemUI = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                    outItem = currentItemUI.Item;
                    currentItemUI.SetItem(item,  1);
                }
                else
                {
                    equipmentSlot.StoreItem(item); // asiganr el objeto a la casilla
                }
                break;
            }
        }
        if(outItem !=null)
        Knapsack.Instance.StoreItem(outItem);
    }

    public void PutOff(Item item)
    {
        Knapsack.Instance.StoreItem(item);
    }

}
