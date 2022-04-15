using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SharedRhythmManager : MonoBehaviour
{
    public static SharedRhythmManager srManager;
    [SerializeField] protected GameObject notes;
    protected RhythmControls rControls;
    [SerializeField] protected float multiplier, distance;
    protected int atbCounter, atbBuildCounter, noteNbr;
    protected float tempo;
    [SerializeField] protected GameObject[] arrows = new GameObject[4];
    [SerializeField] protected GameObject[] activators = new GameObject[4];
    [SerializeField] protected AudioClip[] songs;
    [SerializeField] protected AudioSource song;

    protected virtual void Awake()
    {
        srManager = this;
        rControls = new RhythmControls();
        rControls.Rhythm.Switch.performed += SwitchToCombat;
        EnableRhythmControls();
        StartNotes();
        ChooseSong();
        song.Play();
    }

    protected void ChooseSong()
    {
        int scene = PermManager.pManager.GetSceneFlag();
        switch (scene)
        {
            case 1:
            case 2:
                song.clip = songs[0];
                tempo = 76;
                break;
            case 3:
            case 4:
                song.clip = songs[1];
                tempo = 90;
                break;
            case 5:
                song.clip = songs[2];
                tempo = 115;
                break;
        }
    }

    protected void StartNotes()
    {
        for (int i = 0; i < 25; i++)
        {
            SpawnNote();
        }
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

    public void NoteHit()
    {
        ATBBuildup();
    }

    public void NoteMissed()
    {

    }

    public void PlaceBomb()
    {
        notes.transform.GetChild(4).GetComponent<NoteObj>().SetAsBomb();
    }

    public void SetATB(int atb)
    {
        atbCounter = atb;
    }

    public float GetTempo()
    {
        return tempo / 60;
    }

    public float GetMultiplier()
    {
        return multiplier;
    }

    protected void EnableRhythmControls()
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

    protected void DisableRhythmControls()
    {
        for (int i = 0; i < activators.Length; i++)
        {
            activators[i].SetActive(false);
        }
        rControls.Rhythm.Disable();
    }

    public void BombHit()
    {
        Debug.Log("Ya hit a bomb!");
        if (atbCounter > 0)
        {
            SetATB(atbCounter - 1);
            SharedUIManager.sUIManager.UpdateATBNumber(atbCounter, atbCounter + 1);
        }
        SharedUIManager.sUIManager.StartShake();
    }

    protected void SwitchToCombat(InputAction.CallbackContext obj)
    {
        if (atbCounter >= 1)
        {
            DisableRhythmControls();
            SharedBattleManager.sbManager.StartCombat(atbCounter);
            SharedUIManager.sUIManager.ChangeSwitchDirection();
        }
    }

    protected void ATBBuildup()
    {
        if (atbCounter < 5)
        {
            atbBuildCounter++;
            if (atbBuildCounter == 14)
            {
                atbBuildCounter = 0;
                atbCounter++;
                if (atbCounter == 5)
                {
                    TurnOnShowOffIndicator();
                }
                SharedUIManager.sUIManager.UpdateATBNumber(atbCounter, atbCounter - 1);
            }
            SharedUIManager.sUIManager.UpdateATB(atbBuildCounter);
        }
    }

    private void TurnOnShowOffIndicator()
    {
        if (PermManager.pManager.CheckBossBattle())
        {
            BossUIManager.uiManager.ShowYouCanPressNow();
        }
    }

    public void DeactivateAllControls()
    {
        rControls.Rhythm.Disable();
    }
}
