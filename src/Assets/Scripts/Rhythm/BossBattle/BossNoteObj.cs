using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNoteObj : MonoBehaviour
{
    private bool shouldBePressed;

    void Start()
    {
        shouldBePressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldBePressed)
        {
            NotifyPresed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BossActivator")
        {
            shouldBePressed = true;
        }
        if (collision.tag == "BossMissed")
        {
            NotifyMissed();
        }
    }

    private void NotifyMissed()
    {
        BossRhythmManager.brManager.BossNoteMissed();
        DestroyNote();
    }

    private void NotifyPresed()
    {
        BossRhythmManager.brManager.BossNoteHit();
        DestroyNote();
    }

    private void DestroyNote()
    {
        Destroy(gameObject);
        BossRhythmManager.brManager.SpawnBossNote();
    }
}
