using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item Item { get; set; }
    public int Amount { get; set; }

    //public Text amoutText;
    #region UI component
    private Image itemImage;
    private Text amountText;

    private Image ItemImage
    {
        get
        {
            if(itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }
    private Text AmountText
    {
        get
        {
            if(amountText == null)
            {
                amountText = GetComponentInChildren<Text>();
            }
            return amountText;
        }



    }
    #endregion

    //funcion para resetar el objeto de la casilla(imagen y la cantidad)
    public void SetItem(Item item, int amount = 1)
    {
        this.Item = item;
        this.Amount = amount;
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }
    //funcion para sumar cantidad del objeto
    public void AddAmount(int amount = 1)
    {
        this.Amount += amount;
       
        AmountText.text = Amount.ToString();
    }
    //funcion para restar cantidad del objeto
    public void SubAmount(int amount)
    {
        this.Amount -= amount;
    }
    //Seeter del item UI
    public void SetItemUI(ItemUI itemUI)
    {
        SetItem(itemUI.Item, itemUI.Amount);
    }

    private void Update()
    {
        //amoutText.text = this.amoutText.ToString();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localScale = position;
    }
}
