using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    // Start is called before the first frame update
    public int Damage { get; set; }

    public WeaponType wpType { get; set; }

    public Weapon(int id, string name, ItemType type, Quality quality, string des, int capacity, int buyPrice, int sellPrice, string sprites, int damage,WeaponType wptype)
      : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprites)
    {
        this.Damage = damage;
        this.wpType = wptype;
    }

}

public enum WeaponType
{
    None,
    OffHand,
    MainHand
}
