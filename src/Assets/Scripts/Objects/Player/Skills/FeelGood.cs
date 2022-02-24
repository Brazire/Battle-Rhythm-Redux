using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeelGood : PlayerSkill
{

    public FeelGood()
    {
        icon = Resources.Load<Sprite>("SkillSprites/FeelGood");
    }

    public override void DoAction()
    {
        Player player = BattleManagerRedux.bManager.GetPlayer();
        if (player.HasEnoughATB(2))
        {
            player.UseATB(2);
            player.Heal(player.GetMana() * 10f);
        }
    }
}
