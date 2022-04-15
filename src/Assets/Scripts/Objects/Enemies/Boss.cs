using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : Entity
{
    public Attack baseAttack;
    public Attack bskill2, bskill3, bskill4, uskill;
    private bool isDead;

    public void SetHealthBar()
    {
        isDead = false;
        healthbar.SetMaxHealth(maxhp);
        healthbar.SetHealth(hp);
    }

    public bool CheckDead()
    {
        return isDead;
    }

    public abstract void DoSkill();
    public abstract bool ContinueAttack();
    public abstract bool AbleToHitNote();

    public override void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp < 0)
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

    public override void Die()
    {
        isDead = true;
        SharedBattleManager.sbManager.ReadyMoney((int)(maxhp - 200f));
        BossRhythmManager.brManager.BossDiedStopPlaying();
        SharedBattleManager.sbManager.Victory();
    }
}
