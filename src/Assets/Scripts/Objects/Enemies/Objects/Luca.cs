using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luca : EnemySc
{
    public Luca()
    {
        maxhp = 110f;
        hp = 110f;
        strength = 5f;
        agility = 14f;
        mana = 7f;
        defense = 10f;
    }

    public override float CalculateAttack()
    {
        return 10f + (agility * 2f);
    }

    public override Attack MyBaseAttack()
    {
        return new BasicBitchAttack();
    }

    public override Attack MyFirstSkill()
    {
        return new NoteAttack();
    }

    public override Attack MySecondSkill()
    {
        return null;
    }
}
