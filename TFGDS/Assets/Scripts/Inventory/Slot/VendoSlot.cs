using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Clase para registro de vendas
/// </summary>
public class VendoSlot : Slot
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && InventoryManager.Instance.IsPickItem == true)
        {
            Item currentItem = transform.GetChild(0).GetComponent<ItemUI>().Item;
            transform.parent.parent.SendMessage("SellItem", currentItem);
        }
        else if (eventData.button == PointerEventData.InputButton.Right && InventoryManager.Instance.IsPickItem == false)
        {
            if(transform.childCount > 0)
            {
                Item currentItem = transform.GetChild(0).GetComponent<ItemUI>().Item; // cojo el objeto que esta en el slot
                transform.parent.parent.SendMessage("BuyItem", currentItem); // envio mensaje hacia al padre
            }
        }
    }
}

/*
    
     */
