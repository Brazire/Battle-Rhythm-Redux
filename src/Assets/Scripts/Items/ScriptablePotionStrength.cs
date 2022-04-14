using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Consumable/Strength", order = 251)]
public class ScriptablePotionStrength : ScriptableConsumable
{
    public override void UseItem()
    {
        PermManager.pManager.player.strength += Point;
    }
}
