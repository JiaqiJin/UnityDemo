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

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (transform.childCount > 0)
            //InventoryManager.Instance.HideToolTip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(transform.childCount > 0)
        {
            //string toolTipText = transform.GetChild(0).GetComponent<ItemUI>().Item.GetToolTipText();
            //InventoryManager.Instance.ShowToolTip(toolTipText);
        }
       
    }

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
                if (Input.GetKey(KeyCode.LeftControl))
                {

                }
                else
                {                   
                    //coger el info objeto actual del slot al pickItem siguiendo el mouse
                    InventoryManager.Instance.PickItem.SetItemUI(currentItem);
                    InventoryManager.Instance.IsPickItem = true;
                    Destroy(currentItem.gameObject); // destruir el objeto que ha escogido
                }
            }
        }

    }
}
