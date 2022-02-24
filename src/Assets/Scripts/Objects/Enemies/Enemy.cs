using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public float attackDamage;
    public Attack baseAttack, skill1, skill2;
    public int indexNumber;
    private float atbBuildupTime;
    private EnemyATBBuild builder;

    public Enemy(EnemySc stats, HealthBar healthbar, int indexNumber, EnemyATBBuild builder)
    {
        maxhp = stats.maxhp;
        hp = stats.hp;
        strength = stats.strength;
        agility = stats.agility;
        mana = stats.mana;
        defense = stats.defense;
        attackDamage = stats.CalculateAttack();
        baseAttack = stats.MyBaseAttack();
        skill1 = stats.MyFirstSkill();
        skill2 = stats.MySecondSkill();
        healthbar.SetMaxHealth(maxhp);
        healthbar.SetHealth(hp);
        this.healthbar = healthbar;
        this.indexNumber = indexNumber;
        this.builder = builder;
        builder.SetEnemy(this);
        CalculateAtbTime();
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
    }

    private void CalculateAtbTime()
    {
        atbBuildupTime = 14 - (agility/100 * 12);
        builder.SetMax(atbBuildupTime);
    }

    public void ChoosePostATB()
    {
        Debug.Log("I got a point!");
        atb++;
        if (BattleManagerRedux.bManager.isCombat)
        {
            baseAttack.DoAction((Entity)BattleManagerRedux.bManager.GetPlayer(), this);
        }
        else
        {
            skill1.DoAction((Entity)BattleManagerRedux.bManager.GetPlayer(), this);
        }
        builder.ResetCounter();
    }

    public override float CalculateAttack()
    {
        return attackDamage;
    }

    public override void Die()
    {
        BattleManagerRedux.bManager.RemoveDeadEnemy(this);
    }

    public override void UseATB(int amount)
    {
        atb -= amount;
    }
}
