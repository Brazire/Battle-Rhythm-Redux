using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PermManager : MonoBehaviour
{
    public static PlayerStat player;

    void Awake()
    {
        player = new PlayerStat();
        DontDestroyOnLoad(this);
    }
}
