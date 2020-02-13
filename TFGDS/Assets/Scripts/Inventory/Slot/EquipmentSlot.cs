using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    public WeaponType weaponType;
    public EquipmentType equipmentType;


    private bool IsRightItem(Item item)
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
        //base.OnPointerDown(eventData);
        //si tiene objeto 
        if(InventoryManager.Instance.IsPickItem == true)
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

        }


    
    }

}
