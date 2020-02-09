using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// clase slot incluye los puntero de ratones
/// </summary>
public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IPointerDownHandler
{
    public GameObject itemPrefabs;
    //public ItemUI ItemUI;
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
            itemGameObject.transform.localScale = Vector3.one;
            itemGameObject.transform.localPosition = Vector3.zero;
            itemGameObject.GetComponent<ItemUI>().SetItem(item);
        }
        // sumar la cantidad del item
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

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        /*if (transform.childCount > 0)
        {
            string toolTipText = transform.GetChild(0).GetComponent<ItemUI>().Item.GetToolTipText();
            InventoryManager.Instance.ShowToolTip(toolTipText);
        }*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       /* if (transform.childCount > 0)
            InventoryManager.Instance.HideToolTip();*/

    }
    /// <summary>
    /// Funcion que retorna id del objeto Item
    /// </summary>
    /// <returns></returns>
    public int GetItemId()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
    }
    /// <summary>
    /// Funcion para cambiar la posicion del objeto de slot 
    /// dependiendo si el slot es vacio o no es vacio mediante el teclado ctr para intercambiar
    /// si no es vacio coger el id del objeto y lo intercambia 
    /// si es vacio coge pickeditem y lo pone alli en el hueco
    /// </summary>
    /// <param name="eventData"></param>
    /// 
    public void OnPointerDown(PointerEventData eventData)
    {
        //vacio el slot
     
        //no vacio el slot compruebo los descendiente del slot
        if(transform.childCount > 0)
        {
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>(); // coger el objeto del item 
             //si no ha escogido ningun objketo con el raton
            if(InventoryManager.Instance.IsPickItem == false)
            {
                if (Input.GetKey(KeyCode.LeftControl)) // coger la mitad del objeto actual
                {
                    int amountPicked = (currentItem.Amount + 1) / 2;
                    InventoryManager.Instance.PickUpItem(currentItem.Item, amountPicked);
                    int amoutRemain = currentItem.Amount - amountPicked;
                    if(amoutRemain <= 0) // si no le queda destruye el objeto
                    {
                        Destroy(currentItem.gameObject);
                    }
                    else
                    {
                        currentItem.SetAmount(amoutRemain);
                    }
                }
                else
                {
                    //coger el info objeto actual del slot al pickItem siguiendo el mouse
                    InventoryManager.Instance.PickUpItem(currentItem.Item, currentItem.Amount);
                    Destroy(currentItem.gameObject); // destruir el objeto que ha escogido
                }
            }
            else
            {
                if(currentItem.Item.ID == InventoryManager.Instance.PickItem.Item.ID) // si conincide los objetos en la casilla
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        if(currentItem.Item.Capacity > currentItem.Amount) // comprobar la capacidad el objeto actual esta lleno o no
                        {
                            currentItem.AddAmount();
                            InventoryManager.Instance.RemoveItem();
                        }
                        else
                        {
                            return;
                        }
                    }
                    // si no ha pulsado control pusde deja una parte del objeto en la casilla o todo los objetos
                    else
                    {
                        if(currentItem.Item.Capacity > currentItem.Amount)
                        {
                            int amoutRemaint = currentItem.Item.Capacity - currentItem.Amount; // la capasidad que le queda del slot
                            if(amoutRemaint >= InventoryManager.Instance.PickItem.Amount) // si todavia le queda capacidad para el objetos del inventario
                            {
                                // cantidad actual mas cantidad que nos queda en la manao
                                currentItem.SetAmount(currentItem.Amount + InventoryManager.Instance.PickItem.Amount);
                                InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickItem.Amount);
                            }
                            else
                            {
                                currentItem.SetAmount(currentItem.Amount + amoutRemaint);
                                InventoryManager.Instance.RemoveItem(amoutRemaint);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                // cambios de objetos en la casilla
                else
                {
                    Item item = currentItem.Item;
                    int amount = currentItem.Amount;
                    currentItem.SetItem(InventoryManager.Instance.PickItem.Item, InventoryManager.Instance.PickItem.Amount);
                    InventoryManager.Instance.PickItem.SetItem(item, amount);
                }
            }
        }
        // si es vacio la casilla 
        else
        {
            if(InventoryManager.Instance.IsPickItem == true)
            {
                if (Input.GetKey(KeyCode.LeftControl)) // si pulsa control 
                {
                    // almacenar el item en la casilla
                    this.StoreItem(InventoryManager.Instance.PickItem.Item);
                    InventoryManager.Instance.RemoveItem();
                }
                else
                {
                    // alamace todo los objetos 
                    for(int i = 0; i < InventoryManager.Instance.PickItem.Amount; i++)
                    {
                        this.StoreItem(InventoryManager.Instance.PickItem.Item);
                    }
                    InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickItem.Amount);
                }
            }
            else
            {
                return;
            }
        }

    }
}
