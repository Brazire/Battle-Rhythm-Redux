using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager iManager;
    private Dictionary<ScriptableItem, int> ItemsDict;

    public int currentMoney = 0;

    // Start is called before the first frame update
    void Start()
    {
        iManager = this;
        ItemsDict = new Dictionary<ScriptableItem, int>();
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
        if(ItemsDict.ContainsKey(item) && ItemsDict[item] > 0)
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
