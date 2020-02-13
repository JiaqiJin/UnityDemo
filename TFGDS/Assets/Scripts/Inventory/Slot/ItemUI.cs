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


    private float targetScale = 1.0f;
    //variable para animacion del objeto cuando lo invoca
    private Vector3 animationScale = new Vector3(1.2f, 1.2f, 1.2f);
    
    private void Update()
    {
        //amoutText.text = this.amoutText.ToString();
        if(transform.localScale.x != targetScale) 
        {
            //aumento del tamaño del imagen simulando una animacion
            float scale = Mathf.Lerp(transform.localScale.x, targetScale, 1 * Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    //funcion para resetar el objeto de la casilla(imagen y la cantidad)
    public void SetItem(Item item, int amount = 1)
    {
        transform.localScale = animationScale;
        this.Item = item;
        this.Amount = amount;
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }
    public void SetAmount(int amount)
    {
        transform.localScale = animationScale;
        this.Amount = amount;
        if (Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }

    //funcion para sumar cantidad del objeto
    public void AddAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        this.Amount += amount;

        if (Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }
    //funcion para restar cantidad del objeto
    public void SubAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        this.Amount -= amount;

        if(Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
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
        transform.localPosition = position;
    }
    /// <summary>
    /// Funcion para cambiar posicion del itemui
    /// </summary>
    /// <param name="itemUI"></param>
    public void Exchange(ItemUI itemUI)
    {
        Item tempItem = itemUI.Item;
        int amount = itemUI.Amount;
        itemUI.SetItem(this.Item, this.Amount);
        this.SetItem(tempItem, amount);

    }
 
}
