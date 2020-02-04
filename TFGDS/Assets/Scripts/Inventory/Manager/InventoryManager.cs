using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    #region
    private static InventoryManager instance_;

    public static InventoryManager Instance
    {
        get
        {
            if(instance_ == null)
            {
                instance_ = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return instance_;
        }
        set
        {

        }
    }
    #endregion
    /// <summary>
    /// lista de informacion del item 
    /// </summary>
    private List<Item> itemList;

    private ToolTips tooltip;

    private bool isToolTipShow;

    private Canvas canvas;
    private void Start()
    {
        ParseItemJson();
        tooltip = GameObject.FindObjectOfType<ToolTips>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    private void Update()
    {
        /*if (isToolTipShow)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null,out position);
            tooltip.SetLocalPos(position);
        }*/
    }

    void ParseItemJson()
    {
        itemList = new List<Item>();
        //obtener el objeto texassert del unity
        TextAsset itemText = Resources.Load<TextAsset>("Items");
        string itemJson = itemText.text;
        //print(itemJson);
        JSONObject j = new JSONObject(itemJson);

        foreach (JSONObject item in j.list)
        {
            //Debug.Log(temp["id"].ToString() + temp["name"].ToString());
            string typeStr = item["type"].str;
            //print(typeStr);
            ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), typeStr);

            int id = (int)(item["id"].n);
            string name = item["name"].str;

            string qualityStr = item["quality"].str;
            Quality quality = (Quality)System.Enum.Parse(typeof(Quality), qualityStr);
            int capacity = (int)(item["capacity"].n);
            int buyPrice = (int)(item["buyPrice"].n);
            int sellPrice = (int)(item["sellPrice"].n);
            string sprite = item["sprite"].str;
            string description = item["description"].str;
            

            Item newItem = null;
            switch (type)
            { //int id, string name, ItemType type, Quality quality, string des, int capacity, int buyPrice, int sellPrice, string sprites, int hp, int mp
                case ItemType.Consumible:
                    int hp = (int)(item["hp"].n);
                    int mp = (int)(item["mp"].n);
                    newItem = new Consumible(id, name, type,quality, description, capacity, buyPrice, sellPrice, sprite, hp, mp);
                    break;
                case ItemType.Equipment:
                    //TODO
                    int strength = (int)(item["strength"].n);
                    int intellect = (int)(item["intellect"].n);
                    int agility = (int)(item["agility"].n);
                    int stamina = (int)(item["stamina"].n);
                    EquipmentType equipType = (EquipmentType)System.Enum.Parse(typeof(EquipmentType), item["equipType"].str);
                    newItem = new Equipment(id, name, type,quality, description, capacity, buyPrice, sellPrice, sprite, strength, intellect, agility, stamina, equipType);
                    break;
                case ItemType.Weapon:
                    //TODO
                    int damage = (int)(item["damage"].n);
                    WeaponType wpType = (WeaponType)System.Enum.Parse(typeof(WeaponType), item["weaponType"].str);
                    newItem = new Weapon(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, damage, wpType);
                    break;
                case ItemType.Material:
                    //TODO
                    newItem = new Material(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite);
                    break;

            }
            itemList.Add(newItem);
            //Debug.Log(newItem);
        }
    }

    public Item GetItemById(int id)
    {
        foreach (Item item in itemList)
        {
            if(item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

   /* public void ShowToolTip(string content)
    {
        isToolTipShow = true;
        tooltip.Show(content);
    }*/

    /*public void HideToolTip()
    {
        isToolTipShow = false;
        tooltip.Hide();
    }*/
}
