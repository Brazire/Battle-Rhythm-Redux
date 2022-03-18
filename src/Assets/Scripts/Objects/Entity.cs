using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity
{
    public HealthBar healthbar;
    protected int atb;
    protected float maxhp, hp, strength, agility, mana, defense;

    public abstract void TakeDamage(float amount);

    public virtual void SetATB(int atb)
    {
        this.atb = atb;
    }

    public virtual bool HasEnoughATB(int amount)
    {
        if (atb >= amount)
        {
            return true;
        }
        return false;
    }

    public virtual void Heal(float amount)
    {
        hp += amount;
        if (hp > maxhp)
        {
            hp = maxhp;
        }
        healthbar.SetHealth(hp);
        Debug.Log("Player Has Healed");
    }

    public float GetStrength()
    {
        return strength;
    }

    public float GetAgility()
    {
        return agility;
    }

    public float GetMana()
    {
        return mana;
    }

    public float GetDefense()
    {
        return defense;
    }

    public float GetMaxHp()
    {
        return maxhp;
    }

    public abstract float CalculateAttack();

    public abstract void Die();

    public abstract void UseATB(int amount);
}
