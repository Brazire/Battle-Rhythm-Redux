using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNoteAttack : Attack
{
    public override void DoAction(Entity target, Entity attacker)
    {
        if (attacker.HasEnoughATB(2))
        {
            attacker.UseATB(2);
            BossRhythmManager.brManager.PlaceBomb();
        }
    }
}
