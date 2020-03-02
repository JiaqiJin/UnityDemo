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

    private void InitShop()
    {
        foreach (int itemId in itemIdArray)
        {
            StoreItem(itemId);
        }
    }

    public void BuyItem(Item item)
    {
        bool isSuccess = player.ConsumeCoin(item.BuyPrice);
        if (isSuccess)
        {
            Knapsack.Instance.StoreItem(item);
        }
    }
}
