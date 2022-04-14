using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFuckenDies : Attack
{
    public override void DoAction(Entity target, Entity Attacker)
    {
        if (Attacker.HasEnoughATB(5))
        {
            Attacker.UseATB(5);
            target.Die();
        }
    }
}
