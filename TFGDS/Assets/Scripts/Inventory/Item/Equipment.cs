using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    None,
    Head,
    Neck,
    Chest,
    Ring,
    Leg,
    Bracer,
    Boots,
    Shoulder,
    Belt,
    OffHand,
    Glove

}

public class Equipment : Item
{
    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }
    public EquipmentType EquipType { get; set; }


    public Equipment(int id, string name, ItemType type, Quality quality, string des, int capacity, int buyPrice, int sellPrice, string sprites,
        int strength, int intellect, int agility, int stamina, EquipmentType equiptype)
        :base(id,name,type,quality,des,capacity,buyPrice,sellPrice, sprites)
    {
        this.Strength = strength;
        this.Stamina = stamina;
        this.Intellect = intellect;
        this.Agility = agility;
        this.EquipType = equiptype;
    }
}


