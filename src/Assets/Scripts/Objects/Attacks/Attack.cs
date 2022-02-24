using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack
{
    public virtual void DoAction() { }

    public virtual void DoAction(Entity target, Entity attacker) { }

    public virtual void DoAction(List<Entity> targetParty, int targetIndex, Entity attacker) { }
}
