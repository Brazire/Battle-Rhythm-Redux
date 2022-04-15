using System.Collections.Generic;
using System.Linq;

public class PlayerStat
{
    public float maxHp, hp, strength, agility, mana, defense;
    public int currentMoney;

    private Dictionary<ScriptableItem, int> ItemsDict;
    public Dictionary<ScriptableEquipment.EquipmentType, ScriptableEquipment> equipments;

    public PlayerStat()
    {
        maxHp = 250;
        hp = 250;
        strength = 15;
        agility = 10;
        mana = 32;
        defense = 12; 
        currentMoney = 300;
        ItemsDict = new Dictionary<ScriptableItem, int>();
        equipments = new Dictionary<ScriptableEquipment.EquipmentType, ScriptableEquipment>();
    }

    public void AddEquipment(ScriptableEquipment equip)
    {
        agility += equip.agilityPoint;
        defense += equip.defensePoint;
        strength += equip.strengthPoint;
        equipments[equip.TypeEquipment] = equip;
    }

    public void GiveMoney(int amount)
    {
        currentMoney += amount;
    }

    public void RemoveEquipment(ScriptableEquipment.EquipmentType type)
    {
        var equip = equipments[type];
        agility -= equip.agilityPoint;
        defense -= equip.defensePoint;
        strength -= equip.strengthPoint;
        equipments.Remove(type);
    }

    public int GetQuantity(ScriptableItem item) => ItemsDict[item];
    public bool ItemsExist(ScriptableItem item) => ItemsDict.ContainsKey(item);

    public void AddItemQuantity(ScriptableItem item, int quantities = 1)
    {
        if (ItemsDict.ContainsKey(item))
            ItemsDict[item] += quantities;
        else
            ItemsDict.Add(item, quantities);

        if (ItemsDict[item] == 0)
            ItemsDict.Remove(item);
    }

    public void UseItem(ScriptableItem item)
    {
        if (ItemsDict.ContainsKey(item) && ItemsDict[item] > 0)
        {
            item.UseItem();
            ItemsDict[item]--;
        }

        if (ItemsDict[item] == 0)
            ItemsDict.Remove(item);
    }

    public List<ScriptableItem> GetItems(ScriptableItem.ItemType type)
    {
        return ItemsDict.Where(q => q.Key.GetItemType() == type).Select(i => i.Key).ToList();
    }

    public List<ScriptableItem> GetAllItems()
    {
        return ItemsDict.Select(i => i.Key).ToList();
    }
}
