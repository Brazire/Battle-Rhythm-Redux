using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Consumable/Defence", order = 251)]
public class ScriptablePotionDefence : ScriptableConsumable
{
    public override void UseItem()
    {
        PermManager.pManager.player.defense += Point;
    }
}
