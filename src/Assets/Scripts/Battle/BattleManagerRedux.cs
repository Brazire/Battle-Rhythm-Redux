using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleManagerRedux : MonoBehaviour
{
    public static BattleManagerRedux bManager;
    public bool isCombat;
    private RhythmControls rControls;
    private Player player;
    [SerializeField]private HealthBar playerhealth;
    [SerializeField] private GameObject enemyHolder, target;
    private List<Entity> enemies = new List<Entity>();
    private Vector3 enPos1 = new Vector3(-393f, 2f, 0f);
    private Vector3 enPos2 = new Vector3(-632f, 219f, 0f);
    private Vector3 enPos3 = new Vector3(-635f, -200f, 0f);
    private Vector3 targetPos1 = new Vector3(-230f, 2f, 0f);
    private Vector3 targetPos2 = new Vector3(-468f, 219f, 0f);
    private Vector3 targetPos3 = new Vector3(-471f, -200f, 0f);
    private int enemyCount, targetIndex;

    void Awake()
    {
        bManager = this;
        rControls = new RhythmControls();
        isCombat = false;
        player = new Player(250, 250, 15, 10, 32, 12, playerhealth, false);
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
        SpawnEnemies();
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

    public List<Entity> GetAllEnemies()
    {
        return enemies;
    }

    public Entity GetCurrentTarget()
    {
        return enemies[targetIndex];
    }

    public Player GetPlayer()
    {
        return player;
    }

    private void SwitchToRhythm(InputAction.CallbackContext obj)
    {
        EndCombat();
        RhythmManager.rManager.ContinueRhythm();
        UIManager.uiManager.ChangeSwitchDirection();
    }

    private void RunAway(InputAction.CallbackContext obj)
    {
        UIManager.uiManager.RunPressed();
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        UIManager.uiManager.AttackPressed();
        player.baseAttack.DoAction(enemies[targetIndex], player);
    }

    private void ChooseSkill(InputAction.CallbackContext obj)
    {
        UIManager.uiManager.SkillsPressed();
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
        UIManager.uiManager.CloseSkillMenu();
        UIManager.uiManager.SkillsLetGo();
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
        enemies.Remove(deaded);
        GameObject deadedEnemy = enemyHolder.transform.GetChild(deaded.indexNumber).gameObject;
        deadedEnemy.transform.parent = null;
        Destroy(deadedEnemy);
        if(enemyCount > 0)
        {
            ResetEnemyPos();
            ChangeMethod();
        }
    }

    private void ChangeTarget(InputAction.CallbackContext obj)
    {
        UIManager.uiManager.ChangePressed();
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

    private void ResetEnemyPos()
    {
        for (int x = 1; x <= enemies.Count; x++)
        {
            ((Enemy)enemies[x - 1]).indexNumber = x - 1;
            PlaceEnemy(enemyHolder.transform.GetChild(x - 1).gameObject, x);
        }
    }


    private void SpawnEnemies()
    {
        int count = Random.Range(1, 4);
        for (int x = 0; x < count; x++)
        {
            enemyCount++;
            int type = Random.Range(1, 4);
            switch (type)
            {
                case 1:
                    FindAndCreateEnemy("Luca", new Luca());
                    break;
                case 2:
                    FindAndCreateEnemy("Blobby", new Blobby());
                    break;
                case 3:
                    FindAndCreateEnemy("OrangeJuice", new OrangeJuice());
                    break;
            }
        }
    }

    private void PlaceEnemy(GameObject enemyObj, int index)
    {
        switch (index)
        {
            case 1:
                enemyObj.transform.localPosition = enPos1;
                break;
            case 2:
                enemyObj.transform.localPosition = enPos2;
                break;
            case 3:
                enemyObj.transform.localPosition = enPos3;
                break;
        }
    }

    private void FindAndCreateEnemy(string enemyName, EnemySc enemySc)
    {
        GameObject enemyObj = Instantiate(Resources.Load("Prefabs/Enemies/"+enemyName)) as GameObject;
        enemyObj.transform.SetParent(enemyHolder.transform);
        enemyObj.transform.localScale = new Vector3(1f, 1f, 1f);
        HealthBar enemyHealth = enemyObj.transform.Find("EnemyUI").transform.Find("EnemyHealth").transform.Find("HealthBar").GetComponent<HealthBar>();
        EnemyATBBuild builder = enemyObj.GetComponent<EnemyATBBuild>();
        Enemy enemy = new Enemy(enemySc, enemyHealth, enemyCount - 1, builder);
        enemies.Add(enemy);
        PlaceEnemy(enemyObj, enemyCount);
    }
}
