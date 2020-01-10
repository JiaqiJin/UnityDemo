using System.Collections;
using System.Collections.Generic;

public enum ItemType
{
    Consumible,
    Equipment,
    Weapon,
    Material
};

public enum Quality
{
    Common,
    Uncommom,
    Rare,
    Epic,
    Legendary
};
public class Item 
{
    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType ItemType { get; set; }
    public Quality Quality { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int BuyPrice { get; set; }
    public int Sellprice { get; set; }
    public string Sprite { get; set; }
    public Item(int id, string name, ItemType type, Quality quality, string des, int capacity, int buyPrice, int sellPrice,string sprites)
    {
        this.ID = id;
        this.Name = name;
        this.ItemType = type;
        this.Quality = quality;
        this.Description = des;
        this.Capacity = capacity;
        this.BuyPrice = buyPrice;
        this.Sellprice = sellPrice;
        this.Sprite = sprites;
    }
    /// <summary>
    /// mostrar contenidos
    /// </summary>
    /// <returns></returns>
    public virtual string GetToolTipText()
    {
        return Name;
    } 
}

