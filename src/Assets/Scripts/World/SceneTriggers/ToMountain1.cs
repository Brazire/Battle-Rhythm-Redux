using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMountain1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PermManager.pManager.SetSceneFlag(3);
            PermManager.pManager.LetsLoadAScene();
        }
    }
}
