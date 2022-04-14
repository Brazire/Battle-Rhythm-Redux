using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBossCombat : MonoBehaviour
{
    [SerializeField] private int bossNumValue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player entered a boss area");
        if (collision.tag == "Player")
        {
            PermManager.pManager.SetIsBossBattle();
            PermManager.pManager.SetBossNum(bossNumValue);
            PermManager.pManager.StartBattle();
        }
    }
}
