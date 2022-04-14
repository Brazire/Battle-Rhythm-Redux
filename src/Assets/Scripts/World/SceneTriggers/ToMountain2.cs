using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMountain2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PermManager.pManager.SetSceneFlag(4);
            PermManager.pManager.LetsLoadAScene();
        }
    }
}
