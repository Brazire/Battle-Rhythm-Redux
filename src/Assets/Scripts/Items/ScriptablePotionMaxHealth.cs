using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Consumable/Max Health", order = 251)]
public class ScriptablePotionMaxHealth : ScriptableConsumable
{
    public override void UseItem()
    {
        PermManager.pManager.player.maxHp += Point;
        //CharactersPortraitUpdate.SetPortrait();
    }
}