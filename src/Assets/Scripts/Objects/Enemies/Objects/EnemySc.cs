using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySc
{
    public float maxhp;
    public float hp;
    public float strength;
    public float agility;
    public float mana;
    public float defense;

    public abstract Attack MyBaseAttack();
    public abstract Attack MyFirstSkill();
    public abstract Attack MySecondSkill();
    public abstract float CalculateAttack();
}
