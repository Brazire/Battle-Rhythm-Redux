using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Key Item", order = 251)]
public class ScriptableItem : ScriptableObject
{
    public string itemName;

    [TextArea(3, 10)]
    public string itemDescription;

    public int sellingPrice;
    public int buyingPrice;

    protected virtual ItemType Type => ItemType.keyItem;

    public ItemType GetItemType() => Type;

    public virtual void UseItem() { }

    public enum ItemType
    {
        equipment,
        consumable,
        keyItem
    }
}
