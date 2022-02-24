using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NUp : NoteObj
{
    private void OnEnable()
    {
        rControls.Rhythm.Up.performed += ButtonPressed;
        rControls.Rhythm.Up.Enable();
    }

    private void OnDisable()
    {
        rControls.Rhythm.Up.Disable();
    }
}
