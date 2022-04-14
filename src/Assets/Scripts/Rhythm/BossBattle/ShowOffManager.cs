using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOffManager : MonoBehaviour
{
    public static ShowOffManager soManager;
    [SerializeField] private GameObject notes, bossNotes, activators, bossActivators, uiParent, rhythmParent;
    private int bossCombo, playerCombo, maxCombo, bossMaxCombo, allNotes;
    private bool showingOff, playing;

    public void Awake()
    {
        soManager = this;
        showingOff = false;
        playing = false;
    }

    public void StartShowingOff()
    {
        uiParent.SetActive(true);
        showingOff = true;
        notes.transform.SetParent(uiParent.transform);
        bossNotes.transform.SetParent(uiParent.transform);
        activators.transform.SetParent(uiParent.transform);
        bossActivators.transform.SetParent(uiParent.transform);
        rhythmParent.SetActive(false);
    }

    public void StopShowingOff()
    {
        rhythmParent.SetActive(true);
        showingOff = false;
        SetPlaying(false);
        notes.transform.SetParent(rhythmParent.transform);
        bossNotes.transform.SetParent(rhythmParent.transform);
        activators.transform.SetParent(rhythmParent.transform);
        bossActivators.transform.SetParent(rhythmParent.transform);
        BossUIManager.uiManager.UpdateComboNumber(0, playerCombo);
        BossUIManager.uiManager.UpdateBossComboNumber(0, bossCombo);
        BossUIManager.uiManager.UpdateMaxNumber(0, maxCombo);
        BossUIManager.uiManager.UpdateBossMaxNumber(0, bossMaxCombo);
        DamageSomeone();
        bossCombo = 0;
        playerCombo = 0;
        maxCombo = 0;
        bossMaxCombo = 0;
        allNotes = 0;
        uiParent.SetActive(false);
    }

    private void DamageSomeone()
    {
        if (bossMaxCombo > maxCombo)
        {
            BossUIManager.uiManager.ShowFailure();
            BossBattleManager.bbManager.GetPlayer().TakeDamage(BossBattleManager.bbManager.GetPlayer().GetMaxHp() / 2f);
        }
        else if (maxCombo > bossMaxCombo)
        {
            BossUIManager.uiManager.ShowSuccess();
            BossBattleManager.bbManager.GetBoss().TakeDamage(BossBattleManager.bbManager.GetBoss().GetMaxHp() / 2f);
        }
    }

    public void NotifyHit()
    {
        if (playing)
        {
            playerCombo++;
            BossUIManager.uiManager.UpdateComboNumber(playerCombo, playerCombo - 1);
            allNotes++;
            if (maxCombo < playerCombo)
            {
                maxCombo = playerCombo;
                BossUIManager.uiManager.UpdateMaxNumber(maxCombo, maxCombo - 1);
            }
            CheckShowingOffEnd();
        }
    }

    public void BossNotifyHit()
    {
        if (playing)
        {
            bossCombo++;
            BossUIManager.uiManager.UpdateBossComboNumber(bossCombo, bossCombo - 1);
            if (bossMaxCombo < bossCombo)
            {
                bossMaxCombo = bossCombo;
                BossUIManager.uiManager.UpdateBossMaxNumber(bossMaxCombo, bossMaxCombo - 1);
            }
        }
    }

    public void NotifyMiss()
    {
        if (playing)
        {
            BossUIManager.uiManager.UpdateComboNumber(0, playerCombo);
            playerCombo = 0;
            allNotes++;
            CheckShowingOffEnd();
        }
    }

    public void BossNotifyMiss()
    {
        if (playing)
        {
            BossUIManager.uiManager.UpdateBossComboNumber(0, bossCombo);
            bossCombo = 0;
        }
    }

    private void CheckShowingOffEnd()
    {
        if (allNotes == 15)
        {
            StopShowingOff();
        }
    }

    public bool IsShowingOff()
    {
        return showingOff;
    }

    public void SetPlaying(bool value)
    {
        playing = value;
    }
}
