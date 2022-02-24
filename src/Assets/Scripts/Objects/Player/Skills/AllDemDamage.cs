using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDemDamage : PlayerSkill
{

    public AllDemDamage()
    {
        icon = Resources.Load<Sprite>("SkillSprites/AllDemDamage");
    }

    public override void DoAction()
    {
        Player player = BattleManagerRedux.bManager.GetPlayer();
        if (player.HasEnoughATB(5))
        {
            player.UseATB(5);
            Enemy enemy = (Enemy)BattleManagerRedux.bManager.GetCurrentTarget();
            enemy.Die();
        }
    }
}
