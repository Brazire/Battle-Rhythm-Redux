using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManEater : Boss
{
    public ManEater(HealthBar healthbar)
    {
        maxhp = 400;
        hp = 400;
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

    public override float CalculateAttack()
    {
        return 15 + 2 * agility;
    }

    public override void DoSkill()
    {
        switch (atb)
        {
            case 1:
                baseAttack.DoAction(BossBattleManager.bbManager.GetPlayer(), BossBattleManager.bbManager.GetBoss());
                break;
            case 2:
                bskill2.DoAction(BossBattleManager.bbManager.GetPlayer(), BossBattleManager.bbManager.GetBoss());
                break;
            case 3:
                bskill3.DoAction(BossBattleManager.bbManager.GetPlayer(), BossBattleManager.bbManager.GetBoss());
                break;
            case 4:
                bskill4.DoAction(BossBattleManager.bbManager.GetPlayer(), BossBattleManager.bbManager.GetBoss());
                break;
            case 5:
                uskill.DoAction(BossBattleManager.bbManager.GetPlayer(), BossBattleManager.bbManager.GetBoss());
                break;
        }
    }

    public override bool ContinueAttack()
    {
        int decide = Random.Range(0, 2);
        if (decide == 1)
        {
            return true;
        }
        return false;
    }

    public override bool AbleToHitNote()
    {
        float number = Random.Range(1, 101);
        float limit = (40 - agility) < 0 ? 0 : (40 - agility);
        if (number > limit)
        {
            return true;
        }
        return false;
    }
}
