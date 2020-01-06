using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : Item
{
    public Material(int id, string name, ItemType type, Quality quality, string des, int capacity, int buyPrice, int sellPrice, string sprites)
        : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprites)
    {

    }



}
