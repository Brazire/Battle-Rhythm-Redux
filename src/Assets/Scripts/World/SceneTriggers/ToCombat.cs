using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToCombat : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Random.Range(1, 60) == 1)
            {
                PermManager.pManager.StartBattle();
            }
        }
    }
}
