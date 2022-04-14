using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PermManager : MonoBehaviour
{
    public PlayerStat player;
    private GameObject playerGo;
    private Vector3 playerPos;

    public Dictionary<string, string> varDict { get; private set; }
    public List<int> cinematicAlreadyPlayed;

    public static PermManager pManager;

    private int sceneFlag, bossNum;
    private bool bossFlag, needPlaceFlag;

    void Awake()
    {
        player = new PlayerStat();
        sceneFlag = 1;
        needPlaceFlag = false;
        DontDestroyOnLoad(this.gameObject);
        pManager = this;
        SetIsNotBossBattle();
        LoadVariableDict();
        cinematicAlreadyPlayed = new List<int>();
    }

    public void LoadVariableDict()
    {
        var playerName = "Player";
        varDict = new Dictionary<string, string>
        {
            { "%PlayerName%", playerName }
        };

        var isPlayingWithKeyboard = true;
        if (isPlayingWithKeyboard)
        {
            varDict.Add("%InventoryKey%", "I key");
            varDict.Add("%TalkKey%", "F key");
        }
        else
        {
            varDict.Add("%InventoryKey%", "Start button");
            varDict.Add("%TalkKey%", "A button");
        }
    }

    public int GetSceneFlag()
    {
        return sceneFlag;
    }
    
    public void SetSceneFlag(int value)
    {
        sceneFlag = value;
    }

    public int GetBossNum()
    {
        return bossNum;
    }

    public void SetBossNum(int value)
    {
        bossNum = value;
    }

    public void SetIsBossBattle()
    {
        bossFlag = true;
    }

    public void SetIsNotBossBattle()
    {
        bossFlag = false;
    }

    public void SetPosPlayer(bool value)
    {
        needPlaceFlag = value;
    }

    public void LetsLoadAScene()
    {
        SceneManager.LoadScene(sceneFlag);
    }

    public void PlacePlayerBack()
    {
        if (needPlaceFlag)
        {
            GameObject.Find("Player").transform.position = playerPos;
            needPlaceFlag = false;
        }
    }

    public void ExitCombat()
    {
        if (bossFlag && !needPlaceFlag)
        {
            sceneFlag++;
        }
        LetsLoadAScene();
    }

    public bool CheckBossBattle()
    {
        return bossFlag;
    }

    public void StartBattle()
    {
        if (bossFlag)
        {
            PlayerPosBoss();
            SceneManager.LoadScene("BossBattleScene");
        }
        else
        {
            needPlaceFlag = true;
            playerPos = GameObject.Find("Player").transform.position;
            SceneManager.LoadScene("BattleReduxScene");
        }
    }

    private void PlayerPosBoss()
    {
        switch (bossNum)
        {
            case 1:
                playerPos = new Vector3(451f, 214f, 0f);
                break;
            case 2:
                playerPos = new Vector3(207f, 454f, 0f);
                break;
            case 3:
                playerPos = new Vector3(111f, 1382f, 0f);
                break;
        }
    }
}
