using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SharedBattleManager : MonoBehaviour
{
    public static SharedBattleManager sbManager;
    public bool isCombat;
    protected RhythmControls rControls;
    protected Player player;
    [SerializeField] protected HealthBar playerhealth;
    private int moneyBag;

    protected virtual void Awake()
    {
        sbManager = this;

        rControls = new RhythmControls();
        isCombat = false;
        if (PermManager.pManager.CheckBossBattle())
        {
            player = new Player(PermManager.pManager.player, playerhealth, true);
        }
        else
        {
            player = new Player(PermManager.pManager.player, playerhealth, false);
        }
        rControls.Battle.Run.performed += RunAway;
        rControls.Battle.Skills.performed += ChooseSkill;
        rControls.Battle.Switch.performed += SwitchToRhythm;
        rControls.Battle.Skill1.performed += UseSkill1;
        rControls.Battle.Skill2.performed += UseSkill2;
        rControls.Battle.Skill3.performed += UseSkill3;
        rControls.Battle.SkillBack.performed += BackSkills;
    }

    protected void EnableDefaultControls()
    {
        rControls.Battle.Attack.Enable();
        rControls.Battle.Run.Enable();
        rControls.Battle.Change.Enable();
        rControls.Battle.Skills.Enable();
        rControls.Battle.Switch.Enable();
    }

    protected void DisableDefaultControls()
    {
        rControls.Battle.Attack.Disable();
        rControls.Battle.Run.Disable();
        rControls.Battle.Change.Disable();
        rControls.Battle.Skills.Disable();
        rControls.Battle.Switch.Disable();
    }

    protected void EnableSkillControls()
    {
        rControls.Battle.Skill1.Enable();
        rControls.Battle.Skill2.Enable();
        rControls.Battle.Skill3.Enable();
        rControls.Battle.SkillBack.Enable();
    }

    protected void DisableSkillControls()
    {
        rControls.Battle.Skill1.Disable();
        rControls.Battle.Skill2.Disable();
        rControls.Battle.Skill3.Disable();
        rControls.Battle.SkillBack.Disable();
    }

    public void ReadyMoney(int money)
    {
        moneyBag += money;
    }

    public Player GetPlayer()
    {
        return player;
    }

    protected void SwitchToRhythm(InputAction.CallbackContext obj)
    {
        EndCombat();
        SharedRhythmManager.srManager.ContinueRhythm();
        SharedUIManager.sUIManager.ChangeSwitchDirection();
    }

    protected void RunAway(InputAction.CallbackContext obj)
    {
        SharedUIManager.sUIManager.RunPressed();
    }

    protected void ChooseSkill(InputAction.CallbackContext obj)
    {
        SharedUIManager.sUIManager.SkillsPressed();
        SharedUIManager.sUIManager.OpenSkillMenu();
        DisableDefaultControls();
        EnableSkillControls();
    }

    protected void BackSkills(InputAction.CallbackContext obj)
    {
        SharedUIManager.sUIManager.CloseSkillMenu();
        SharedUIManager.sUIManager.SkillsLetGo();
        DisableSkillControls();
        EnableDefaultControls();
    }

    protected void UseSkill1(InputAction.CallbackContext obj)
    {
        player.UseSkill(1);
    }

    protected void UseSkill2(InputAction.CallbackContext obj)
    {
        player.UseSkill(2);
    }

    protected void UseSkill3(InputAction.CallbackContext obj)
    {
        player.UseSkill(3);
    }

    public void StartCombat(int playerATB)
    {
        player.SetATB(playerATB);
        isCombat = true;
        EnableDefaultControls();
    }

    public void EndCombat()
    {
        DisableDefaultControls();
        isCombat = false;
    }

    public void Victory()
    {
        FuckControls();
        PermManager.pManager.GivePlayerMoney(moneyBag);
        SharedUIManager.sUIManager.StartVictory();
    }

    public void Defeat()
    {
        FuckControls();
        PermManager.pManager.SetPosPlayer(true);
        SharedUIManager.sUIManager.StartDefeat();
    }

    private void FuckControls()
    {
        SharedRhythmManager.srManager.DeactivateAllControls();
        SharedBattleManager.sbManager.DeactivateAllControls();
    }

    public void WeAllDie()
    {
        PermManager.pManager.UpdateHealthValue(player.GetHp());
        PermManager.pManager.ExitCombat();
    }

    public void DeactivateAllControls()
    {
        rControls.Battle.Disable();
    }
}
