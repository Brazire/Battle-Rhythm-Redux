using System.Collections.Generic;

public class PlayerStat
{
    public float maxHp, hp, strength, agility, mana, defense;

    public Dictionary<ScriptableEquipment.EquipmentType, ScriptableEquipment> equipments;

    public PlayerStat()
    {
        equipments = new Dictionary<ScriptableEquipment.EquipmentType, ScriptableEquipment>();
    }

    public void AddEquipment(ScriptableEquipment equip)
    {
        agility += equip.agilityPoint;
        defense += equip.defensePoint;
        strength += equip.strengthPoint;
        equipments[equip.TypeEquipment] = equip;
    }

    public void RemoveEquipment(ScriptableEquipment.EquipmentType type)
    {
        var equip = equipments[type];
        agility -= equip.agilityPoint;
        defense -= equip.defensePoint;
        strength -= equip.strengthPoint;
        equipments.Remove(type);
    }
}
