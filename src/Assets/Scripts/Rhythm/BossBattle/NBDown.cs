using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBDown : BPNoteObj
{
    private void OnEnable()
    {
        rControls.Rhythm.Down.performed += ButtonPressed;
        rControls.Rhythm.Down.Enable();
    }

    private void OnDisable()
    {
        rControls.Rhythm.Down.Disable();
    }
}
