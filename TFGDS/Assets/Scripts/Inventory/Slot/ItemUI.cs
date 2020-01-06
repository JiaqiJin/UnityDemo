using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item Item { get; set; }
    public int Amount { get; set; }
    //public Text amoutText;

    public void SetItem(Item item, int amount = 1)
    {
        this.Item = item;
        this.Amount = amount;
        
    }

    public void AddAmount(int amount = 1)
    {
        this.Amount += amount;
        
    }

    private void Update()
    {
        //amoutText.text = this.amoutText.ToString();
    }
}
