using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Consumable/Life", order = 251)]
public class ScriptablePotionLife : ScriptableConsumable
{
    /// <summary>
    /// Restore the player life and update the health displayed.
    /// </summary>
    public override void UseItem()
    {
        PermManager.pManager.player.hp = Mathf.Clamp(PermManager.pManager.player.hp + Point, 0, PermManager.pManager.player.maxHp);
        PermManager.pManager.UpdatePlayerHealth();
    }
}