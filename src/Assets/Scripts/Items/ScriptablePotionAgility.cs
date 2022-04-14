using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Consumable/Agility", order = 251)]
public class ScriptablePotionAgility : ScriptableConsumable
{
    public override void UseItem()
    {
        PermManager.pManager.player.agility += Point;
    }
}
