using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : Entity
{
    public Attack baseAttack;
    public Attack bskill2, bskill3, bskill4, uskill;

    public void SetHealthBar()
    {
        healthbar.SetMaxHealth(maxhp);
        healthbar.SetHealth(hp);
    }

    public abstract void DoSkill();
    public abstract bool ContinueAttack();
    public abstract bool AbleToHitNote();

    public override void TakeDamage(float amount)
    {
        hp -= amount;
        if (amount < 0)
        {
            hp = 0;
            Die();
        }
        healthbar.SetHealth(hp);
        BossUIManager.uiManager.StartShake();
    }

    public override void UseATB(int amount)
    {
        atb -= amount;
        BossRhythmManager.brManager.SetBossAtb(atb);
        BossUIManager.uiManager.UpdateBossATBNumber(atb, atb + amount);
    }
}
