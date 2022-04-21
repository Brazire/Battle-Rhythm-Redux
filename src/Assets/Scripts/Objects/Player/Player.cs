using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    //Basic player Object, uses Entity.
    public Attack baseAttack;
    private PlayerSkill skill1, skill2, skill3;
    public List<Attack> skills = new List<Attack>();
    private bool isBossFight;

    public Player(PlayerStat stats, HealthBar healthbar, bool isBossFight)
    {
        this.maxhp = stats.maxHp;
        this.hp = stats.hp;
        this.strength = stats.strength;
        this.agility = stats.agility;
        this.mana = stats.mana;
        this.defense = stats.defense;
        this.healthbar = healthbar;
        baseAttack = new BasicBitchAttack();
        this.isBossFight = isBossFight;
        if (this.isBossFight)
        {
            skill1 = new BossFeelGood();
            skill2 = new FUBossNotes();
            skill3 = new BossAllDemDamage();
        }
        else
        {
            skill1 = new FeelGood();
            skill2 = new EveryoneHurts();
            skill3 = new AllDemDamage();
        }
        SharedUIManager.sUIManager.SetSkillIcon(1, skill1.GetIcon());
        SharedUIManager.sUIManager.SetSkillIcon(2, skill2.GetIcon());
        SharedUIManager.sUIManager.SetSkillIcon(3, skill3.GetIcon());
        healthbar.SetMaxHealth(maxhp);
        healthbar.SetHealth(hp);
        atb = 0;
    }

    public override void TakeDamage(float amount)
    {
        hp -= (amount / (defense / 2));
        if (hp <= 0f)
        {
            hp = 1f;
            Die();
        }
        healthbar.SetHealth(hp);
        SharedUIManager.sUIManager.StartShake();
    }

    public override float CalculateAttack()
    {
        return 10f + (2f * strength);
    }

    public override void Die()
    {
        SharedBattleManager.sbManager.Defeat();
        Debug.Log("Player has died.");
    }

    public override void UseATB(int amount)
    {
        if (atb == 5 && PermManager.pManager.CheckBossBattle())
        {
            BossUIManager.uiManager.ShowYouCantPressNow();
        }
        atb -= amount;
        SharedRhythmManager.srManager.SetATB(atb);
        SharedUIManager.sUIManager.UpdateATBNumber(atb, atb + amount);
    }
    
    public void UseSkill(int number)
    {
        switch (number)
        {
            case 1:
                CheckToUseSkill(skill1);
                break;
            case 2:
                CheckToUseSkill(skill2);
                break;
            case 3:
                CheckToUseSkill(skill3);
                break;
        }
    }

    private void CheckToUseSkill(Attack skill)
    {
        if (skill != null)
        {
            skill.DoAction();
        }
    }
}