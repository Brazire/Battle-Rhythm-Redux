using System.Collections.Generic;

public class PlayerStat
{
    public float maxHp, hp, strength, agility, mana, defense;

    public Dictionary<ScriptableEquipment.EquipmentType, ScriptableEquipment> equipments;

    public PlayerStat()
    {
        maxHp = 250;
        hp = 250;
        strength = 15;
        agility = 10;
        mana = 32;
        defense = 12;
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
