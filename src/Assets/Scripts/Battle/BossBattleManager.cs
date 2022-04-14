using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossBattleManager : MonoBehaviour
{
    public static BossBattleManager bbManager;
    public bool isCombat;
    private RhythmControls rControls;
    private Player player;
    [SerializeField] private HealthBar playerhealth, bosshealth;
    private Boss boss;

    void Awake()
    {
        bbManager = this;
        rControls = new RhythmControls();
        isCombat = false;
        player = new Player(250, 250, 15, 10, 32, 12, playerhealth, true);
        boss = new ManEater(bosshealth);
        rControls.Battle.Attack.performed += PerformAttack;
        rControls.Battle.Run.performed += RunAway;
        rControls.Battle.Skills.performed += ChooseSkill;
        rControls.Battle.Switch.performed += SwitchToRhythm;
        rControls.Battle.Skill1.performed += UseSkill1;
        rControls.Battle.Skill2.performed += UseSkill2;
        rControls.Battle.Skill3.performed += UseSkill3;
        rControls.Battle.SkillBack.performed += BackSkills;
    }

    private void EnableDefaultControls()
    {
        rControls.Battle.Attack.Enable();
        rControls.Battle.Run.Enable();
        rControls.Battle.Change.Enable();
        rControls.Battle.Skills.Enable();
        rControls.Battle.Switch.Enable();
    }

    private void DisableDefaultControls()
    {
        rControls.Battle.Attack.Disable();
        rControls.Battle.Run.Disable();
        rControls.Battle.Change.Disable();
        rControls.Battle.Skills.Disable();
        rControls.Battle.Switch.Disable();
    }

    private void EnableSkillControls()
    {
        rControls.Battle.Skill1.Enable();
        rControls.Battle.Skill2.Enable();
        rControls.Battle.Skill3.Enable();
        rControls.Battle.SkillBack.Enable();
    }

    private void DisableSkillControls()
    {
        rControls.Battle.Skill1.Disable();
        rControls.Battle.Skill2.Disable();
        rControls.Battle.Skill3.Disable();
        rControls.Battle.SkillBack.Disable();
    }

    public Boss GetBoss()
    {
        return boss;
    }

    public Player GetPlayer()
    {
        return player;
    }

    private void SwitchToRhythm(InputAction.CallbackContext obj)
    {
        EndCombat();
        BossRhythmManager.brManager.ContinueRhythm();
        BossUIManager.uiManager.ChangeSwitchDirection();
    }

    private void RunAway(InputAction.CallbackContext obj)
    {
        BossUIManager.uiManager.RunPressed();
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        BossUIManager.uiManager.AttackPressed();
        player.baseAttack.DoAction(boss, player);
    }

    private void ChooseSkill(InputAction.CallbackContext obj)
    {
        BossUIManager.uiManager.SkillsPressed();
        BossUIManager.uiManager.OpenSkillMenu();
        DisableDefaultControls();
        EnableSkillControls();
    }


    private void UseSkill1(InputAction.CallbackContext obj)
    {
        player.UseSkill(1);
    }

    private void UseSkill2(InputAction.CallbackContext obj)
    {
        player.UseSkill(2);
    }

    private void UseSkill3(InputAction.CallbackContext obj)
    {
        player.UseSkill(3);
    }

    private void BackSkills(InputAction.CallbackContext obj)
    {
        BossUIManager.uiManager.CloseSkillMenu();
        BossUIManager.uiManager.SkillsLetGo();
        DisableSkillControls();
        EnableDefaultControls();
    }

    public void StartCombat(int playerATB)
    {
        player.SetATB(playerATB);
        isCombat = true;
        EnableDefaultControls();
    }

    public void StartBossAttack(int bossATB)
    {
        boss.SetATB(bossATB);
        StartCoroutine(BossAttacking());
    }

    private IEnumerator BossAttacking()
    {
        yield return new WaitForSeconds(1f);
        boss.DoSkill();
        yield return new WaitForSeconds(1f);
        BossRhythmManager.brManager.BossResumePlaying();
    }

    public void EndCombat()
    {
        DisableDefaultControls();
        isCombat = false;
    }
}
