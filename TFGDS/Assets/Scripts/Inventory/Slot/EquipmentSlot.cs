using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    public WeaponType weaponType;
    public EquipmentType equipmentType;


    public bool IsRightItem(Item item)
    {
        return (item is Weapon && ((Weapon)item).wpType == this.weaponType)
            || (item is Equipment && ((Equipment)item).EquipType == this.equipmentType);
    }

    /// <summary>
    /// cambia la funcion para equipar armaduras
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerDown(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (InventoryManager.Instance.IsPickItem == false && transform.childCount > 0)
            {
                ItemUI currenItemUI = transform.GetChild(0).GetComponent<ItemUI>();

                transform.parent.SendMessage("PutOff", currenItemUI.Item);
                Destroy(currenItemUI.gameObject);
                
            }
        }


        if (eventData.button != PointerEventData.InputButton.Left) return; // si es izq button no selecciona objetos
        //base.OnPointerDown(eventData);
        //si tiene objeto 
        if (InventoryManager.Instance.IsPickItem == true)
        {
            ItemUI pickItem = InventoryManager.Instance.PickItem;
            if(transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>(); // objeto actual del slot de armaduras


                if (IsRightItem(pickItem.Item))
                {
                    InventoryManager.Instance.PickItem.Exchange(currentItemUI);
                }
            }
            // no existe armaduras en el slot
            else
            {
                if (IsRightItem(pickItem.Item))
                {
                    this.StoreItem(InventoryManager.Instance.PickItem.Item); // coger el objeto actual
                    InventoryManager.Instance.RemoveItem(1);
                }
            }
        }
        //si no contiene ningu objeto
        else
        {
            if(transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                InventoryManager.Instance.PickUpItem(currentItemUI.Item, currentItemUI.Amount);
                Destroy(transform.GetChild(0).gameObject);
            }
        }


    
    }

}
