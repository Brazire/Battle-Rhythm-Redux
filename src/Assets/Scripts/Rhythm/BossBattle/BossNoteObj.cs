using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNoteObj : MonoBehaviour
{
    //Similar to NoteObj, but for the boss' notes
    private bool shouldBePressed, isBomb;
    [SerializeField] private SpriteRenderer render;

    void Start()
    {
        isBomb = false;
        shouldBePressed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BossActivator")
        {
            if (BossRhythmManager.brManager.IsBossRhythmPlaying() && BossBattleManager.bbManager.GetBoss().AbleToHitNote())
            {
                NotifyPresed();
            }
        }
        else if (collision.tag == "BossMissed")
        {
            NotifyMissed();
        }
    }

    public void SetAsBomb()
    {
        isBomb = true;
        render.color = new Color(0f, 0f, 125f);
    }

    private void NotifyMissed()
    {
        if (ShowOffManager.soManager.IsShowingOff())
        {
            ShowOffManager.soManager.BossNotifyMiss();
        }
        else
        {
            BossRhythmManager.brManager.BossNoteMissed();
        }
        DestroyNote();
    }

    private void NotifyPresed()
    {
        if (isBomb)
        {
            BossRhythmManager.brManager.BossBombHit();
        }
        else
        {
            if (ShowOffManager.soManager.IsShowingOff())
            {
                ShowOffManager.soManager.BossNotifyHit();
            }
            else
            {
                BossRhythmManager.brManager.BossNoteHit();
            }
        }
        DestroyNote();
    }

    private void DestroyNote()
    {
        Destroy(gameObject);
        BossRhythmManager.brManager.SpawnBossNote();
    }
}
