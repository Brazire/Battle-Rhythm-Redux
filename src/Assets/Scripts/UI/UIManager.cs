using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SharedUIManager
{
    public static UIManager uiManager;

    protected override void Awake()
    {
        base.Awake();
        uiManager = this;
        switchAnim = switchUi.GetComponent<Animator>();
        for (int i = 0; i < atbnumUi.transform.childCount; i++)
        {
            GameObject child = atbnumUi.transform.GetChild(i).gameObject;
            if (child != null)
            {
                child.SetActive(false);
            }
        }
        atbnumUi.transform.GetChild(0).gameObject.SetActive(true);
    }
}
