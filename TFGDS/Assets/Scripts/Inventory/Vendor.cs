using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase para vender objetos
/// </summary>
public class Vendor : Inventory
{

    public int[] itemIdArray;
    private PlayerInfo player;

    public override void Start()
    {
        base.Start();
        InitShop();
        player = GameObject.Find("PlayerIU").GetComponent<PlayerInfo>();
    }
    /// <summary>
    /// Methosdos para inicializar objetos en un array de item segun su id
    /// </summary>
    private void InitShop()
    {
        foreach (int itemId in itemIdArray)
        {
            StoreItem(itemId);
        }
    }
    /// <summary>
    /// Methos para comprar objetos
    /// lo añade al inventario
    /// </summary>
    /// <param name="item"></param>
    public void BuyItem(Item item)
    {
        bool isSuccess = player.ConsumeCoin(item.BuyPrice);
        if (isSuccess)
        {
            Knapsack.Instance.StoreItem(item);
        }
    }
    /// <summary>
    /// Methosdos para vender objetos
    /// </summary>
    /// <param name="item"></param>
    public void SellItem(Item item)
    {
        int sellAmount = 1;
        // para controlar la cantidad que quieres vender
        if (Input.GetKey(KeyCode.LeftControl))
        {
            sellAmount = 1;
        }
        else
        {
            sellAmount = InventoryManager.Instance.PickItem.Amount;
        }

        int countAmount = InventoryManager.Instance.PickItem.Item.Sellprice * sellAmount;
        player.EarnCoin(countAmount);
 
        InventoryManager.Instance.RemoveItem(sellAmount);

    }
}
