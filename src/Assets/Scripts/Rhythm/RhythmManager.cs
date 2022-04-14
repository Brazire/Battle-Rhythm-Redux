using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RhythmManager : SharedRhythmManager
{
    public static RhythmManager rManager;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        rManager = this;
    }
}