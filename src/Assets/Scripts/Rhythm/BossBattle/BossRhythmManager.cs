using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossRhythmManager : MonoBehaviour
{
    public static BossRhythmManager brManager;
    [SerializeField] private GameObject notes, bossNotes;
    private RhythmControls rControls;
    [SerializeField] private float multiplier, tempo, distance;
    private int atbCounter, bossatbCounter, atbBuildCounter, bossatbBuildCounter, noteNbr, bossNoteNbr;
    [SerializeField] private GameObject[] arrows = new GameObject[4];
    [SerializeField] private GameObject[] bossArrows = new GameObject[4];
    [SerializeField] private GameObject[] activators = new GameObject[4];
    [SerializeField] private AudioSource song;
    private bool bossPlaying;

    // Start is called before the first frame update
    private void Awake()
    {
        brManager = this;
        rControls = new RhythmControls();
        rControls.Rhythm.Switch.performed += SwitchToCombat;
        EnableRhythmControls();
        StartNotes();
        StartBossNotes();
        song.Play();
    }

    private void StartNotes()
    {
        for (int i = 0; i < 25; i++)
        {
            SpawnNote();
        }
    }

    private void StartBossNotes()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnBossNote();
        }
        bossPlaying = true;
    }

    public void SpawnNote()
    {
        int ranNum = Random.Range(0, 4);
        GameObject newArrow = Instantiate(arrows[ranNum], notes.transform);
        Vector3 pos = newArrow.transform.localPosition;
        pos.y += (noteNbr * distance);
        newArrow.transform.localPosition = pos;
        noteNbr++;
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

    public void NoteHit()
    {
        ATBBuildup();
    }

    public void NoteMissed()
    {
        
    }

    public void BossNoteHit()
    {
        BossATBBuildup();
    }

    public void BossNoteMissed()
    {
        
    }

    public void BombHit()
    {
        Debug.Log("Ya hit a bomb!");
        if (atbCounter > 0)
        {
            atbCounter -= 1;
            SetATB(atbCounter);
            BossUIManager.uiManager.UpdateATBNumber(atbCounter, atbCounter + 1);
        }
        BossUIManager.uiManager.StartShake();
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

    public void PlaceBomb()
    {
        notes.transform.GetChild(4).GetComponent<BPNoteObj>().SetAsBomb();
    }

    public void PlaceBossBomb()
    {
        bossNotes.transform.GetChild(4).GetComponent<BossNoteObj>().SetAsBomb();
    }

    public void SetATB(int atb)
    {
        atbCounter = atb;
    }

    public void SetBossAtb(int atb)
    {
        bossatbCounter = atb;
    }

    public float GetTempo()
    {
        return tempo / 60;
    }

    public float GetMultiplier()
    {
        return multiplier;
    }

    public bool IsBossRhythmPlaying()
    {
        return bossPlaying;
    }

    private void EnableRhythmControls()
    {
        for (int i = 0; i < activators.Length; i++)
        {
            activators[i].SetActive(true);
        }
        rControls.Rhythm.Enable();
    }

    public void ContinueRhythm()
    {
        EnableRhythmControls();
    }

    private void SwitchToCombat(InputAction.CallbackContext obj)
    {
        if (atbCounter >= 1 && !ShowOffManager.soManager.IsShowingOff())
        {
            DisableRhythmControls();
            BossBattleManager.bbManager.StartCombat(atbCounter);
            BossUIManager.uiManager.ChangeSwitchDirection();
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

    private void DisableRhythmControls()
    {
        for (int i = 0; i < activators.Length; i++)
        {
            activators[i].SetActive(false);
        }
        rControls.Rhythm.Disable();
    }

    private void ATBBuildup()
    {
        if (atbCounter < 5)
        {
            atbBuildCounter++;
            if (atbBuildCounter == 14)
            {
                atbBuildCounter = 0;
                atbCounter++;
                BossUIManager.uiManager.UpdateATBNumber(atbCounter, atbCounter - 1);
            }
            BossUIManager.uiManager.UpdateATB(atbBuildCounter);
        }
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