using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleManagerRedux : SharedBattleManager
{
    public static BattleManagerRedux bManager;
    [SerializeField] private GameObject enemyHolder, target;
    private List<Entity> enemies = new List<Entity>();
    private Vector3 enPos1 = new Vector3(-393f, 2f, 0f);
    private Vector3 enPos2 = new Vector3(-632f, 219f, 0f);
    private Vector3 enPos3 = new Vector3(-635f, -200f, 0f);
    private Vector3 targetPos1 = new Vector3(-230f, 2f, 0f);
    private Vector3 targetPos2 = new Vector3(-468f, 219f, 0f);
    private Vector3 targetPos3 = new Vector3(-471f, -200f, 0f);
    private int enemyCount, targetIndex;

    protected override void Awake()
    {
        base.Awake();
        bManager = this;
        targetIndex = 0;
        rControls.Battle.Attack.performed += PerformAttack;
        rControls.Battle.Change.performed += ChangeTarget;
        SpawnEnemies();
    }

    public List<Entity> GetAllEnemies()
    {
        return enemies;
    }

    public Entity GetCurrentTarget()
    {
        return enemies[targetIndex];
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        UIManager.uiManager.AttackPressed();
        player.baseAttack.DoAction(enemies[targetIndex], player);
    }

    //Delete dead enemy object and remove the sprite.
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
        else
        {
            Victory();
        }
    }

    private void ChangeTarget(InputAction.CallbackContext obj)
    {
        UIManager.uiManager.ChangePressed();
        ChangeMethod();
    }

    //Used to place the target sprite
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

    //Used to shuffle enemy when one dies
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

    //Used to create an enemy GameObject and place it in the game world
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
