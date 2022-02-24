using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteAttack : Attack
{
    public override void DoAction(Entity target, Entity attacker)
    {
        if (attacker.HasEnoughATB(1))
        {
            attacker.UseATB(1);
            RhythmManager.rManager.PlaceBomb();
        }
    }
}
