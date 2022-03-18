using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOffStart : MonoBehaviour
{
    [SerializeField] private GameObject showOffSide;
    [SerializeField] private bool rightSide;
    [SerializeField] private float orPos, olPos;

    public void StartShowOff()
    {
        Debug.Log("Thing started moving.");
        StartCoroutine(MoveObject());
    }

    public void ResetPosition()
    {
        if (rightSide)
        {
            showOffSide.transform.localPosition = new Vector3(orPos, 0f, 0f);
        }
        else
        {
            showOffSide.transform.localPosition = new Vector3(olPos, 0f, 0f);
        }
    }

    private IEnumerator MoveObject()
    {
        if (rightSide)
        {
            int center = 400;
            while(showOffSide.transform.localPosition.x > center)
            {
                showOffSide.transform.localPosition -= new Vector3(10f, 0f, 0f);
                yield return null;
            }
        }
        else
        {
            int center = -400;
            while (showOffSide.transform.localPosition.x < center)
            {
                showOffSide.transform.localPosition += new Vector3(10f, 0f, 0f);
                yield return null;
            }
        }
        BossUIManager.uiManager.NotifyHalfCenter();
    }
}
