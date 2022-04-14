using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDesert1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PermManager.pManager.SetSceneFlag(5);
            PermManager.pManager.LetsLoadAScene();
        }
    }
}
