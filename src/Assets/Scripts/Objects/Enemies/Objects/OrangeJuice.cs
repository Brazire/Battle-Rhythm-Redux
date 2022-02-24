using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeJuice : EnemySc
{
    public OrangeJuice()
    {
        maxhp = 120f;
        hp = 120f;
        strength = 20f;
        agility = 5f;
        mana = 7f;
        defense = 22f;
    }

    public override float CalculateAttack()
    {
        return 10f + (strength * 2f);
    }

    public override Attack MyBaseAttack()
    {
        return new BasicBitchAttack();
    }

    public override Attack MyFirstSkill()
    {
        return new BlindAttack();
    }

    public override Attack MySecondSkill()
    {
        return null;
    }
}
