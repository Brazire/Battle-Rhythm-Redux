using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToForest2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PermManager.pManager.SetSceneFlag(2);
            PermManager.pManager.LetsLoadAScene();
        }
    }
}
