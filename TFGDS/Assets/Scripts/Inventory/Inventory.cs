using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    protected Slot[] slotList;

    private float targetAlpha = 1;

    private float smoothing = 4;

    private bool isInventoryOpen;

    public bool IsInventoryOpen
    {
        get { return isInventoryOpen; }
        set { value = isInventoryOpen; }
    }

    private CanvasGroup canvasGroup;

    public CanvasGroup CanvasGroups { get { return canvasGroup; } }

    // Start is called before the first frame update
    public virtual void Start()
    {
        slotList = GetComponentsInChildren<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if(canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if(Mathf.Abs(canvasGroup.alpha - targetAlpha ) < 0.1f)
            {
                canvasGroup.alpha = targetAlpha;                
            }
           
        }
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

    /// <summary>
    /// Mostrar inventario
    /// // si blockrayscast es verdad  se interactual con el panel 
    /// </summary>
    public void Show()
    {
        targetAlpha = 1;
        canvasGroup.blocksRaycasts = true;
        IsInventoryOpen = true;
    }
    /// <summary>
    /// Ocultar inventario
    /// // si blockrayscast es falso no se interactual con el panel 
    /// </summary>
    public void Hide()
    {
        targetAlpha = 0;
        canvasGroup.blocksRaycasts = false; 
        isInventoryOpen = false;
    }

    public void DisplaySwitch()
    {
        //print("es " + IsInventoryOpen);
        if(targetAlpha == 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

}
