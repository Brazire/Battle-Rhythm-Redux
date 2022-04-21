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

    /// <summary>
    /// This method add stat to the player when he equip an equipment.
    /// </summary>
    /// <param name="equip">The ScriptableEquipment the player want to equip.</param>
    public void AddEquipment(ScriptableEquipment equip)
    {
        agility += equip.agilityPoint;
        defense += equip.defensePoint;
        strength += equip.strengthPoint;
        equipments[equip.TypeEquipment] = equip;
    }

    /// <summary>
    /// Give a certain amount of money to the player.
    /// </summary>
    /// <param name="amount">The amount of money to add.</param>
    public void GiveMoney(int amount)
    {
        currentMoney += amount;
    }

    /// <summary>
    /// This method remove the stat the equipment was giving to the player.
    /// </summary>
    /// <param name="type">The ScriptableEquipment to remove</param>
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

    /// <summary>
    /// This method will add or remove quantity of an item from the player inventory. 
    /// If the quantity reach 0, the item will be removed from the inventory.
    /// </summary>
    /// <param name="item">The item to add/remove quantity from</param>
    /// <param name="quantities">The quantity to add or remove.</param>
    public void AddItemQuantity(ScriptableItem item, int quantities = 1)
    {
        if (ItemsDict.ContainsKey(item))
            ItemsDict[item] += quantities;
        else
            ItemsDict.Add(item, quantities);

        if (ItemsDict[item] == 0)
            ItemsDict.Remove(item);
    }

    /// <summary>
    /// This method use an item from the inventory of the player. It will call the UseItem 
    /// method from the ScriptableItem and remove one of the quantity from the inventory.
    /// </summary>
    /// <param name="item">The item the player will use.</param>
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

    /// <summary>
    /// This method is used to get a list of only one type of ScriptableItem. (Example, get all equipment)
    /// </summary>
    /// <param name="type">The type of ScriptableItem to return.</param>
    /// <returns>A list of ScriptableItem of the requested type.</returns>
    public List<ScriptableItem> GetItems(ScriptableItem.ItemType type)
    {
        return ItemsDict.Where(q => q.Key.GetItemType() == type).Select(i => i.Key).ToList();
    }

    /// <summary>
    /// This method is to get all the ScriptableItem the player have. Used to sell in a shop.
    /// </summary>
    /// <returns>A list of ScriptableItem.</returns>
    public List<ScriptableItem> GetAllItems()
    {
        return ItemsDict.Select(i => i.Key).ToList();
    }
}
