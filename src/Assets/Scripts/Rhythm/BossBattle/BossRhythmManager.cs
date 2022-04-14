using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossRhythmManager : SharedRhythmManager
{
    public static BossRhythmManager brManager;
    [SerializeField] private GameObject bossNotes;
    private int bossatbCounter, bossatbBuildCounter, bossNoteNbr;
    [SerializeField] private GameObject[] bossArrows = new GameObject[4];
    private bool bossPlaying;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        brManager = this;
        rControls.Rhythm.ShowOff.performed += StartShowOff;
        StartBossNotes();
    }

    private void StartBossNotes()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnBossNote();
        }
        bossPlaying = true;
    }

    public void SpawnBossNote()
    {
        int ranNum = Random.Range(0, 4);
        GameObject newArrow = Instantiate(bossArrows[ranNum], bossNotes.transform);
        Vector3 pos = newArrow.transform.localPosition;
        pos.y += (bossNoteNbr * distance);
        newArrow.transform.localPosition = pos;
        bossNoteNbr++;
    }

    public void BossNoteHit()
    {
        BossATBBuildup();
    }

    public void BossNoteMissed()
    {
        
    }

    public void BossBombHit()
    {
        if (bossatbCounter > 0)
        {
            bossatbCounter -= 1;
            SetBossAtb(bossatbCounter);
            BossUIManager.uiManager.UpdateBossATBNumber(bossatbCounter, bossatbCounter + 1);
        }
    }

    public void PlaceBossBomb()
    {
        bossNotes.transform.GetChild(4).GetComponent<BossNoteObj>().SetAsBomb();
    }

    public void SetBossAtb(int atb)
    {
        bossatbCounter = atb;
    }

    public bool IsBossRhythmPlaying()
    {
        return bossPlaying;
    }

    private void StartShowOff(InputAction.CallbackContext obj)
    {
        if (atbCounter == 5)
        {
            atbCounter = 0;
            BossUIManager.uiManager.UpdateATBNumber(atbCounter, 5);
            BossUIManager.uiManager.ShowYouCantPressNow();
            BossUIManager.uiManager.StartShowOff();
        }
    }

    private void BossSwitchToCombat()
    {
        bossPlaying = false;
        BossUIManager.uiManager.ChangeBossSwitchDirection();
        BossBattleManager.bbManager.StartBossAttack(bossatbCounter);
    }

    public void BossResumePlaying()
    {
        bossPlaying = true;
        BossUIManager.uiManager.ChangeBossSwitchDirection();
    }

    public void BossDiedStopPlaying()
    {
        bossPlaying = false;
    }

    private void BossATBBuildup()
    {
        if (bossatbCounter < 5)
        {
            bossatbBuildCounter++;
            if (bossatbBuildCounter == 14)
            {
                bossatbBuildCounter = 0;
                bossatbCounter++;
                BossUIManager.uiManager.UpdateBossATBNumber(bossatbCounter, bossatbCounter - 1);
                if (!BossBattleManager.bbManager.GetBoss().ContinueAttack())
                {
                    BossSwitchToCombat();
                }
            }
            BossUIManager.uiManager.UpdateBossATB(bossatbBuildCounter);
        }
    }
}
