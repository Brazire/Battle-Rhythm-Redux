using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : SharedUIManager
{
    //UI manager used only when it's a boss battle.
    public static BossUIManager uiManager;
    [SerializeField] private GameObject bossSwitchUi, bossAtbUI, bossatbnumUi, comboNum, bossComboNum, maxNum, bossMaxNum, canPressNow, success, failure;
    private Animator bossSwitchAnim;
    [SerializeField] private Sprite[] bossAtbUiSprites = new Sprite[14];
    [SerializeField] private ShowOffStart left, right;
    [SerializeField] private ShowOffShow showCase;
    private bool facingRight = false;
    private int halfCenter;

    protected override void Awake()
    {
        base.Awake();
        uiManager = this;
        switchAnim = switchUi.GetComponent<Animator>();
        bossSwitchAnim = bossSwitchUi.GetComponent<Animator>();
    }

    public void StartShowOff()
    {
        left.StartShowOff();
        right.StartShowOff();
    }

    public void ShowSuccess()
    {
        success.SetActive(true);
        StartCoroutine(ShowOffResult(success));
    }

    public void ShowFailure()
    {
        failure.SetActive(true);
        StartCoroutine(ShowOffResult(failure));
    }

    //Used to show the result message after a show off
    private IEnumerator ShowOffResult(GameObject message)
    {
        message.SetActive(true);
        SpriteRenderer renderer = message.GetComponent<SpriteRenderer>();
        while (renderer.color.a < 1f)
        {
            renderer.color += new Color(0f, 0f, 0f, 0.05f);
            message.transform.position += new Vector3(0.05f, 0f, 0f);
            yield return new WaitForSeconds(0.01f);
        }
        renderer.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(3f);
        renderer.color = new Color(1f, 1f, 1f, 0);
        message.transform.position = new Vector3(205, 60, 255);
        message.SetActive(false);
    }

    //Used to check if the two blocks that move in when starting the show off mechanic have reached the center
    public void NotifyHalfCenter()
    {
        halfCenter++;
        if (halfCenter == 2)
        {
            halfCenter = 0;
            showCase.ShowCase();
            left.ResetPosition();
            right.ResetPosition();
        }
    }

    //Used to show you can use the show off mode now
    public void ShowYouCanPressNow()
    {
        canPressNow.SetActive(true);
    }

    //Used to hide the show off mode button
    public void ShowYouCantPressNow()
    {
        canPressNow.SetActive(false);
    }

    public void UpdateBossATB(int number)
    {
        bossAtbUI.GetComponent<Image>().sprite = bossAtbUiSprites[number];
    }

    //Same as shared UIManager, but with the boss ui element.
    public void ChangeBossSwitchDirection()
    {
        if (facingRight)
        {
            bossSwitchAnim.Play("Move-left");
            facingRight = false;
        }
        else
        {
            bossSwitchAnim.Play("Move-right");
            facingRight = true;
        }
    }

    public void UpdateBossATBNumber(int number, int curNumber)
    {
        NumberUpdate(bossatbnumUi, number, curNumber);
    }


    public void UpdateComboNumber(int number, int curNumber)
    {
        NumberUpdate(comboNum, number, curNumber);
    }

    public void UpdateBossComboNumber(int number, int curNumber)
    {
        NumberUpdate(bossComboNum, number, curNumber);
    }

    public void UpdateMaxNumber(int number, int curNumber)
    {
        NumberUpdate(maxNum, number, curNumber);
    }

    public void UpdateBossMaxNumber(int number, int curNumber)
    {
        NumberUpdate(bossMaxNum, number, curNumber);
    }
}
