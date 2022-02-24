using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindAttack : Attack
{
    public override void DoAction(Entity target, Entity attacker)
    {
        if (attacker.HasEnoughATB(1))
        {
            attacker.UseATB(1);
            UIManager.uiManager.HideNotes(8f);
        }
    }
}
