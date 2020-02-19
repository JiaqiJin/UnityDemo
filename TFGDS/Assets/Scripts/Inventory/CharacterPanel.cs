using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : Inventory
{

    private static CharacterPanel instance_;
    public static CharacterPanel Instance
    {
        get
        {
            if (instance_ == null)
            {
                //Debug.Log()
                instance_ = GameObject.Find("CharacterPanel").GetComponent<CharacterPanel>();

            }
            return instance_;
        }
    }

    private Text propertyText;
    public PlayerInfo playerInfo;
    public override void Start()
    {
        base.Start();
        propertyText = transform.Find("PropertyPanel/Text").GetComponent<Text>();
        UpdatePropertytText();
    }

    /// <summary>
    /// Metodos para poner armaduras en la casilla del player
    /// </summary>
    /// <param name="item"></param>
    public void PutOn(Item item)
    {
        Item outItem = null;
        foreach(Slot slot in slotList)
        {
            EquipmentSlot equipmentSlot = (EquipmentSlot)slot; // transforma en qup slot
            if (equipmentSlot.IsRightItem(item))
            {
                if(equipmentSlot.transform.childCount > 0) // ya tiene armaduras
                {
                    ItemUI currentItemUI = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                    outItem = currentItemUI.Item;
                    currentItemUI.SetItem(item,  1);
                }
                else
                {
                    equipmentSlot.StoreItem(item); // asiganr el objeto a la casilla
                }
                break;
            }
        }
        if(outItem !=null)
        Knapsack.Instance.StoreItem(outItem);

        UpdatePropertytText();
    }
    /// <summary>
    /// Metodos para remover objeto del playerr
    /// </summary>
    /// <param name="item"></param>
    public void PutOff(Item item)
    {
        Knapsack.Instance.StoreItem(item);
        UpdatePropertytText();
    }


    private void UpdatePropertytText()
    {
        int strength = 0, intellect = 0, agility = 0, stamina = 0, power = 0;
        foreach (EquipmentSlot slot in slotList)
        {
            if(slot.transform.childCount >0) // si hay obejtos en la casilla
            {
                Item item = slot.transform.GetChild(0).GetComponent<ItemUI>().Item;
                if(item is Equipment)
                {
                    Equipment e = (Equipment)item;
                    strength += e.Strength;
                    intellect += e.Intellect;
                    agility += e.Agility;
                    stamina += e.Stamina;
                    
                }
                else if (item is Weapon)
                {
                    power += ((Weapon)item).Damage;
                }
            }
        }

        strength += playerInfo.BasicStrength;
        intellect += playerInfo.BasicIntellect;
        agility += playerInfo.BasicAgility;
        stamina += playerInfo.Stamina;
        power += playerInfo.Power;
        string text = string.Format("Stength：{0}\nIntellect：{1}\nAgility：{2}\nStamina：{3}\nDamage：{4} ", strength, intellect, agility, stamina, power);
        propertyText.text = text;
    }
}
