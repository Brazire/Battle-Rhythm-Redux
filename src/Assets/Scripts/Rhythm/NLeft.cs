using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NLeft : NoteObj
{
    private void OnEnable()
    {
        rControls.Rhythm.Left.performed += ButtonPressed;
        rControls.Rhythm.Left.Enable();
    }

    private void OnDisable()
    {
        rControls.Rhythm.Left.Disable();
    }
}
