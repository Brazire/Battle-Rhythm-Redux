using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Equipment", order = 251)]
public class ScriptableEquipment : ScriptableItem
{
    protected override ItemType Type => ItemType.equipment;

    public EquipmentType TypeEquipment;
    public int defensePoint;
    public int agilityPoint;
    public int strengthPoint;

    public override void UseItem()
    {
        if (PermManager.player.equipments.ContainsKey(TypeEquipment))
        {
            InventoryManager.iManager.AddItemQuantity(PermManager.player.equipments[TypeEquipment]);
            PermManager.player.RemoveEquipment(TypeEquipment);
        }

        PermManager.player.AddEquipment(this);
    }

    public enum EquipmentType
    {
        head,
        torso,
        legs,
        boots,
        weapon
    }
}
