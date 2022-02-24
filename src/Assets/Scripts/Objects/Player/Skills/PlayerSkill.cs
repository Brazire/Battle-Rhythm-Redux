using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSkill : Attack
{
    protected Sprite icon;

    public virtual Sprite GetIcon()
    {
        return icon;
    }
}
