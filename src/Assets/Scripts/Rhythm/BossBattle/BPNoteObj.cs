using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BPNoteObj : MonoBehaviour
{
    public bool shouldBePressed, isEarly, isBomb;
    protected RhythmControls rControls;
    [SerializeField] private SpriteRenderer render;

    private void Awake()
    {
        isBomb = false;
        rControls = new RhythmControls();
    }

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

    private void NotifyHit()
    {
        if (ShowOffManager.soManager.IsShowingOff())
        {
            ShowOffManager.soManager.NotifyHit();
        }
        else
        {
            BossRhythmManager.brManager.NoteHit();
        }
        DestroyNote();
    }

    private void NotifyBombHit()
    {
        BossRhythmManager.brManager.BombHit();
        DestroyNote();
    }

    private void NotifyMissed()
    {
        if (ShowOffManager.soManager.IsShowingOff())
        {
            ShowOffManager.soManager.NotifyMiss();
        }
        else
        {
            BossRhythmManager.brManager.NoteMissed();
        }
        DestroyNote();
    }

    private void DestroyNote()
    {
        Destroy(gameObject);
        BossRhythmManager.brManager.SpawnNote();
    }
}
