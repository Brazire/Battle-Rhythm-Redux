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

    //Decide which boss to play based on area
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
                return new DestroyerOfNoobs(bosshealth);
        }
        return null;
    }

    public Boss GetBoss()
    {
        return boss;
    }

    // Perform bosses' attack
    private void PerformAttack(InputAction.CallbackContext obj)
    {
        BossUIManager.uiManager.AttackPressed();
        player.baseAttack.DoAction(boss, player);
    }

    //Is called when the boss switches from rhythm mode to battle mode.
    public void StartBossAttack(int bossATB)
    {
        boss.SetATB(bossATB);
        StartCoroutine(BossAttacking());
    }

    //Routine to delay the attack a bit
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
