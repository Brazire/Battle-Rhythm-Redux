using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoreOuch : Attack
{
    public override void DoAction(Entity target, Entity attacker)
    {
        if (attacker.HasEnoughATB(3))
        {
            attacker.UseATB(3);
            target.TakeDamage(50f + attacker.GetMana() * 0.25f);
        }
    }
}
