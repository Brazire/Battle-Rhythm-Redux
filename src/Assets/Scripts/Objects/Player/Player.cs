using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Attack baseAttack;
    private PlayerSkill skill1, skill2, skill3;
    public List<Attack> skills = new List<Attack>();
    private bool isBossFight;

    public Player(int maxhp, int hp, int strength, int agility, int mana, int defense, HealthBar healthbar, bool isBossFight)
    {
        this.maxhp = maxhp;
        this.hp = hp;
        this.strength = strength;
        this.agility = agility;
        this.mana = mana;
        this.defense = defense;
        this.healthbar = healthbar;
        baseAttack = new BasicBitchAttack();
        this.isBossFight = isBossFight;
        if (this.isBossFight)
        {
            skill1 = new BossFeelGood();
            skill2 = new FUBossNotes();
            skill3 = new BossAllDemDamage();
            BossUIManager.uiManager.SetSkillIcon(1, skill1.GetIcon());
            BossUIManager.uiManager.SetSkillIcon(2, skill2.GetIcon());
            BossUIManager.uiManager.SetSkillIcon(3, skill3.GetIcon());
        }
        else
        {
            skill1 = new FeelGood();
            skill2 = new EveryoneHurts();
            skill3 = new AllDemDamage();
            UIManager.uiManager.SetSkillIcon(1, skill1.GetIcon());
            UIManager.uiManager.SetSkillIcon(2, skill2.GetIcon());
            UIManager.uiManager.SetSkillIcon(3, skill3.GetIcon());
        }
        healthbar.SetMaxHealth(maxhp);
        healthbar.SetHealth(hp);
        atb = 0;
    }

    public override void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0f)
        {
            hp = 0f;
            Die();
        }
        healthbar.SetHealth(hp);
        if (isBossFight)
        {
            BossUIManager.uiManager.StartShake();
        }
        else
        {
            UIManager.uiManager.StartShake();
        }
    }

    public override float CalculateAttack()
    {
        return 10f + (2f * strength);
    }

    public override void Die()
    {
        Debug.Log("Player has died.");
    }

    public override void UseATB(int amount)
    {
        atb -= amount;
        if (isBossFight)
        {
            BossRhythmManager.brManager.SetATB(atb);
            BossUIManager.uiManager.UpdateATBNumber(atb, atb + amount);
        }
        else
        {
            RhythmManager.rManager.SetATB(atb);
            UIManager.uiManager.UpdateATBNumber(atb, atb + amount);
        }
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