using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerOfNoobs : Boss
{
    public DestroyerOfNoobs(HealthBar healthbar)
    {
        maxhp = 99999;
        hp = 99999;
        strength = 12;
        mana = 20;
        agility = 18;
        defense = 17;
        this.healthbar = healthbar;
        baseAttack = new BasicBitchAttack();
        bskill2 = new BossNoteAttack();
        bskill3 = new BossMoreOuch();
        bskill4 = new BossDemHeals();
        uskill = new PlayerFuckenDies();
        SetHealthBar();
    }

    public override bool AbleToHitNote()
    {
        return true;
    }

    public override float CalculateAttack()
    {
        return 99999f;
    }

    public override bool ContinueAttack()
    {
        if (atb != 5)
        {
            return false;
        }
        return true;
    }

    public override void DoSkill()
    {
        uskill.DoAction(BossBattleManager.bbManager.GetPlayer(), BossBattleManager.bbManager.GetBoss());
    }
}
