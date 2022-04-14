using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDemHeals : Attack
{
    public override void DoAction(Entity target, Entity Attacker)
    {
        if (Attacker.HasEnoughATB(4))
        {
            Attacker.UseATB(4);
            Attacker.Heal(Attacker.GetMaxHp() * 0.25f);
        }
    }
}
