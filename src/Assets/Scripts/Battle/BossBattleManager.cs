using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossBattleManager : SharedBattleManager
{
    public static BossBattleManager bbManager;
    [SerializeField] private HealthBar bosshealth;
    private Boss boss;

    protected override void Awake()
    {
        base.Awake();
        bbManager = this;
        boss = ChooseBoss();
        rControls.Battle.Attack.performed += PerformAttack;
    }

    private Boss ChooseBoss()
    {
        int boss = PermManager.pManager.GetBossNum();
        switch (boss)
        {
            case 1:
                return new ManEater(bosshealth);
            case 2:
                return new ManEater(bosshealth);
            case 3:
                return new ManEater(bosshealth);
        }
        return null;
    }

    public Boss GetBoss()
    {
        return boss;
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        BossUIManager.uiManager.AttackPressed();
        player.baseAttack.DoAction(boss, player);
    }

    public void StartBossAttack(int bossATB)
    {
        boss.SetATB(bossATB);
        StartCoroutine(BossAttacking());
    }

    private IEnumerator BossAttacking()
    {
        yield return new WaitForSeconds(1f);
        if (!boss.CheckDead())
        {
            boss.DoSkill();
            yield return new WaitForSeconds(1f);
            BossRhythmManager.brManager.BossResumePlaying();
        }
    }
}
