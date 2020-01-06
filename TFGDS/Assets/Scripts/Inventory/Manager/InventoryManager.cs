using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
        ParseItemJson();
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
            int ID = (int)(item["id"].n);
            string name = item["name"].str;

            string qualityStr = item["quality"].str;
            Quality quality = (Quality)System.Enum.Parse(typeof(Quality), qualityStr);
            int capacity = (int)(item["capacity"].n);
            int buyPrice = (int)(item["buyPrice"].n);
            int sellPrice = (int)(item["sellPrice"].n);
            string description = item["description"].str;
            string sprite = item["sprite"].str;

            Item newItem = null;
            switch (type)
            { //int id, string name, ItemType type, Quality quality, string des, int capacity, int buyPrice, int sellPrice, string sprites, int hp, int mp
                case ItemType.Consumible:
                    int hp = (int)(item["hp"].n);
                    int mp = (int)(item["mp"].n);
                    newItem = new Consumible(ID, name, type,quality, description, capacity, buyPrice, sellPrice, sprite, hp, mp);
                    break;

            }
            itemList.Add(newItem);
            //Debug.Log(itemList.ToString());
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
}
