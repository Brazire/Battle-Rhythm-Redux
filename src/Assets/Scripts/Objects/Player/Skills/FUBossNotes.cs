using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FUBossNotes : PlayerSkill
{
    public FUBossNotes()
    {
        icon = Resources.Load<Sprite>("SkillSprites/FUBossNotes");
    }

    public override void DoAction()
    {
        Player player = BossBattleManager.bbManager.GetPlayer();
        if (player.HasEnoughATB(3))
        {
            player.UseATB(3);
            BossRhythmManager.brManager.PlaceBossBomb();
        }
    }
}
