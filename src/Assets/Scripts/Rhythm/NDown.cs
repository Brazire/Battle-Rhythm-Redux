using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NDown : NoteObj
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
