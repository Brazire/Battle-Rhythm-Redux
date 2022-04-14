using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PermManager : MonoBehaviour
{
    public static Dictionary<string, string> varDict { get; private set; }
    public static PlayerStat player;

    void Awake()
    {
        player = new PlayerStat();
        DontDestroyOnLoad(this);

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
}
