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
    [SerializeField] private HealthBar playerhealth;
    [SerializeField] private GameObject enemyHolder, target;
    private Boss boss;
    private Vector3 enPos1 = new Vector3(-393f, 2f, 0f);
    private Vector3 enPos2 = new Vector3(-632f, 219f, 0f);
    private Vector3 enPos3 = new Vector3(-635f, -200f, 0f);
    private Vector3 targetPos1 = new Vector3(-230f, 2f, 0f);
    private Vector3 targetPos2 = new Vector3(-468f, 219f, 0f);
    private Vector3 targetPos3 = new Vector3(-471f, -200f, 0f);
    private int enemyCount, targetIndex;

    void Awake()
    {
        bbManager = this;
        rControls = new RhythmControls();
        isCombat = false;
        player = new Player(250, 250, 15, 10, 32, 12, playerhealth, true);
        targetIndex = 0;
        rControls.Battle.Attack.performed += PerformAttack;
        rControls.Battle.Run.performed += RunAway;
        rControls.Battle.Change.performed += ChangeTarget;
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

    public Entity GetBoss()
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
        RhythmManager.rManager.ContinueRhythm();
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
        UIManager.uiManager.OpenSkillMenu();
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

    public void EndCombat()
    {
        DisableDefaultControls();
        isCombat = false;
    }


    public void RemoveDeadEnemy(Enemy deaded)
    {
        enemyCount--;
        targetIndex = -1;
        GameObject deadedEnemy = enemyHolder.transform.GetChild(deaded.indexNumber).gameObject;
        deadedEnemy.transform.parent = null;
        Destroy(deadedEnemy);
        if (enemyCount > 0)
        {
            ChangeMethod();
        }
    }

    private void ChangeTarget(InputAction.CallbackContext obj)
    {
        BossUIManager.uiManager.ChangePressed();
        ChangeMethod();
    }

    private void ChangeMethod()
    {
        targetIndex++;
        if (targetIndex == enemyCount)
        {
            targetIndex = 0;
        }
        switch (targetIndex)
        {
            case 0:
                target.transform.localPosition = targetPos1;
                break;
            case 1:
                target.transform.localPosition = targetPos2;
                break;
            case 2:
                target.transform.localPosition = targetPos3;
                break;
        }
    }
}
