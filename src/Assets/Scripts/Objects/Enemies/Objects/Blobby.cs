using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blobby : EnemySc
{
    public Blobby()
    {
        maxhp = 150f;
        hp = 150f;
        strength = 4f;
        agility = 5f;
        mana = 20f;
        defense = 16f;
    }

    public override float CalculateAttack()
    {
        return 10f + (mana * 2f);
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
