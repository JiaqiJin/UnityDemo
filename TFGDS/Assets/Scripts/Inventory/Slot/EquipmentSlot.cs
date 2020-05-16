using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    public WeaponType weaponType; // tipos de weapons
    public EquipmentType equipmentType; // tipos de equipment

    /// <summary>
    /// Metodos para comprobar si pertenece al tipo de objeto
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
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
                Item itemTepm = currenItemUI.Item;
                // poner en el inventario
                DestroyImmediate(currenItemUI.gameObject);
                transform.parent.SendMessage("PutOff", itemTepm);              
                
            }
        }


        if (eventData.button != PointerEventData.InputButton.Left) return; // si es izq button no selecciona objetos
                                                                           //base.OnPointerDown(eventData);

        bool isUpdateProperty = false;
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
                    isUpdateProperty = true;
                }
            }
            // no existe armaduras en el slot
            else
            {
                if (IsRightItem(pickItem.Item))
                {
                    this.StoreItem(InventoryManager.Instance.PickItem.Item); // coger el objeto actual
                    InventoryManager.Instance.RemoveItem(1);
                    isUpdateProperty = true;
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
                isUpdateProperty = true;
            }
        }
        //enviar mensaje(funcion) hacia arriba
        if (isUpdateProperty)
        {
            transform.SendMessageUpwards("UpdatePropertytText");
        }
    
    }

}
