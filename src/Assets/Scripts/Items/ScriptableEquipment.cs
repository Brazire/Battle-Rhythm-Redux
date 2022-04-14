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
        if (PermManager.pManager.player.equipments.ContainsKey(TypeEquipment))
        {
            PermManager.pManager.player.AddItemQuantity(PermManager.pManager.player.equipments[TypeEquipment]);
            PermManager.pManager.player.RemoveEquipment(TypeEquipment);
        }

        PermManager.pManager.player.AddEquipment(this);
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
