using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumible : Item
{
    public int HP;
    public int MP;

    public Consumible(int id, string name, ItemType type, Quality quality, string des, int capacity, int buyPrice, int sellPrice, string sprites, int hp, int mp)
       : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprites)
    {
        HP = hp;
        MP = mp;
    }

    public override string ToString()
    {
        string s = "";
        s += ID.ToString();
        s += Quality;
        s += Description;
        s += Capacity;
        s += ItemType;
        s += BuyPrice;
        s += Sellprice;
        s += MP;
        s += HP;

        return s;
    }

}
