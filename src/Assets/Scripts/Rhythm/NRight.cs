using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NRight : NoteObj
{
    private void OnEnable()
    {
        rControls.Rhythm.Right.performed += ButtonPressed;
        rControls.Rhythm.Right.Enable();
    }

    private void OnDisable()
    {
        rControls.Rhythm.Right.Disable();
    }
}
