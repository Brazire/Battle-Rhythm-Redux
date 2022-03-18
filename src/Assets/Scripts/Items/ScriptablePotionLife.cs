using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Consumable/Life", order = 251)]
public class ScriptablePotionLife : ScriptableConsumable
{
    public override void UseItem()
    {
        PermManager.player.hp = Mathf.Clamp(PermManager.player.hp + Point, 0, PermManager.player.maxHp);
        //CharactersPortraitUpdate.SetPortrait();
    }
}