using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Slot[] slotList;

    // Start is called before the first frame update
    public virtual void Start()
    {
        slotList = GetComponentsInChildren<Slot>();
    }

    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }
    
    /// <summary>
    /// Funcion para guardar items
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool StoreItem(Item item)
    {
        if(item == null)
        {
            Debug.LogWarning("No existe el ITEM id !!!!");
            return false;
        }
        if(item.Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if(slot == null)
            {
                Debug.LogWarning("No hay mas huecos !!");
                return false;
            }
            else
            {
                slot.StoreItem(item); // alamcena el objeto de el slot del inventario
            }
        }
        else
        {
            Slot slot = FindSameIdSlot(item);
            if(slot != null)
            {
                slot.StoreItem(item);
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if(emptySlot != null)
                {
                    emptySlot.StoreItem(item);
                }
                else
                {
                    Debug.LogWarning("No hay mas huecos !!");
                    return false;
                }
            }
        }
        return true;
    }
    /// <summary>
    /// Metodos para encontrar huecos en los slots
    /// </summary>
    /// <returns></returns>
    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            if(slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }
    /// <summary>
    /// Funcion para encontrar el mismo huecos
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private Slot FindSameIdSlot(Item item)
    {
        foreach(Slot slot in slotList)
        {
            if(slot.transform.childCount >= 1 && slot.GetItemId() == item.ID && slot.IsFilled() == false)
            {
                return slot;
            }
        }
        return null;
    }

}
