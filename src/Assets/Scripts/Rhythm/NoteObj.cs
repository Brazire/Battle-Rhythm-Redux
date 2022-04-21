using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class NoteObj : MonoBehaviour
{
    //Base object used by notes
    public bool shouldBePressed, isEarly, isBomb;
    protected RhythmControls rControls;
    [SerializeField] private SpriteRenderer render;

    private void Awake()
    {
        isBomb = false;
        rControls = new RhythmControls();
    }

    //Called when the note button is pressed, checks if it should be pressed and notifies when a hit happens.
    protected void ButtonPressed(InputAction.CallbackContext obj)
    {
        if (shouldBePressed)
        {
            if (isBomb)
            {
                NotifyBombHit();
            }
            else
            {
                if (PermManager.pManager.CheckBossBattle())
                {
                    if (ShowOffManager.soManager.IsShowingOff())
                    {

                    }
                }
                NotifyHit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D activator)
    {
        if (activator.tag == "Early")
        {
            isEarly = true;
        }
        if (activator.tag == "Activator")
        {
            isEarly = false;
            shouldBePressed = true;
        }
        if (activator.tag == "Missed")
        {
            shouldBePressed = false;
            NotifyMissed();
        }
    }

    public void SetAsBomb()
    {
        isBomb = true;
        render.color = new Color(125f, 0f, 0f);
    }

    //Calls the correct hit, (the show off one or the normal one)
    private void NotifyHit()
    {
        if (PermManager.pManager.CheckBossBattle() && ShowOffManager.soManager.IsShowingOff())
        {
            ShowOffManager.soManager.NotifyHit();
        }
        else
        {
            SharedRhythmManager.srManager.NoteHit();
        }
        DestroyNote();
    }

    private void NotifyBombHit()
    {
        SharedRhythmManager.srManager.BombHit();
        DestroyNote();
    }

    private void NotifyMissed()
    {
        SharedRhythmManager.srManager.NoteMissed();
        DestroyNote();
    }

    private void DestroyNote()
    {
        Destroy(gameObject);
        SharedRhythmManager.srManager.SpawnNote();
    }
}
