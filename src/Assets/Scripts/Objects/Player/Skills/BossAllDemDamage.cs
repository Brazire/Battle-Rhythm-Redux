using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAllDemDamage : PlayerSkill
{

    public BossAllDemDamage()
    {
        icon = Resources.Load<Sprite>("SkillSprites/AllDemDamage");
    }

    public override void DoAction()
    {
        Player player = BossBattleManager.bbManager.GetPlayer();
        if (player.HasEnoughATB(5))
        {
            player.UseATB(5);
            BossBattleManager.bbManager.GetBoss().Die();
        }
    }
}
