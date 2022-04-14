using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFeelGood : PlayerSkill
{
    public BossFeelGood()
    {
        icon = Resources.Load<Sprite>("SkillSprites/BossFeelGood");
    }

    public override void DoAction()
    {
        Player player = BossBattleManager.bbManager.GetPlayer();
        if (player.HasEnoughATB(2))
        {
            player.UseATB(2);
            player.Heal(player.GetMana() * 10f);
        }
    }
}
