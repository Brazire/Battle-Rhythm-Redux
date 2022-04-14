using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : MonoBehaviour
{
    public static BossUIManager uiManager;
    [SerializeField] private GameObject switchUi, bossSwitchUi, atbUI, bossAtbUI, bossatbnumUi, atbnumUi, actionUI, skillSelectUI, skill1, skill2, skill3, comboNum, bossComboNum;
    private Animator switchAnim, bossSwitchAnim;
    [SerializeField] private Sprite[] atbUiSprites = new Sprite[14];
    [SerializeField] private Sprite[] bossAtbUiSprites = new Sprite[14];
    [SerializeField] private Sprite[] actions = new Sprite[5];
    [SerializeField] private GameObject hider;
    [SerializeField] private GameObject camera;
    [SerializeField] private ShowOffStart left, right;
    [SerializeField] private ShowOffShow showCase;
    private bool facingLeft = false;
    private bool facingRight = false;
    private float hideTimer = 0f;
    private int halfCenter;

    private void Awake()
    {
        uiManager = this;
        switchAnim = switchUi.GetComponent<Animator>();
        bossSwitchAnim = bossSwitchUi.GetComponent<Animator>();
    }

    public void StartShowOff()
    {
        left.StartShowOff();
        right.StartShowOff();
    }

    private void Update()
    {
        if (hideTimer > 0f)
        {
            hideTimer -= 1 * Time.deltaTime;
        }
        else if (hider.activeSelf)
        {
            hider.SetActive(false);
            hideTimer = 0f;
        }
    }

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

    public void StartShake()
    {
        camera.GetComponent<CameraShaker>().StartShake();
    }

    public void UpdateATB(int number)
    {
        atbUI.GetComponent<Image>().sprite = atbUiSprites[number];
    }

    public void UpdateBossATB(int number)
    {
        bossAtbUI.GetComponent<Image>().sprite = bossAtbUiSprites[number];
    }

    public void ChangeSwitchDirection()
    {
        if (facingLeft)
        {
            switchAnim.Play("Move-right");
            facingLeft = false;
        }
        else
        {
            switchAnim.Play("Move-Left");
            facingLeft = true;
        }
    }

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

    public void SetSkillIcon(int number, Sprite icon)
    {
        switch (number)
        {
            case 1:
                skill1.GetComponent<SpriteRenderer>().sprite = icon;
                break;
            case 2:
                skill2.GetComponent<SpriteRenderer>().sprite = icon;
                break;
            case 3:
                skill3.GetComponent<SpriteRenderer>().sprite = icon;
                break;
        }
    }

    public void UpdateATBNumber(int number, int curNumber)
    {
        NumberUpdate(atbnumUi, number, curNumber);
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

    private void NumberUpdate(GameObject toUpdate, int number, int curNumber)
    {
        toUpdate.transform.GetChild(curNumber).gameObject.SetActive(false);
        toUpdate.transform.GetChild(number).gameObject.SetActive(true);
    }

    public void HideNotes(float timer)
    {
        hider.SetActive(true);
        hideTimer = timer;
    }

    public void AttackPressed()
    {
        StartCoroutine(PressButton(actions[1], actions[0], actionUI));
    }

    public void SkillsLetGo()
    {
        actionUI.GetComponent<Image>().sprite = actions[0];
    }

    public void SkillsPressed()
    {
        actionUI.GetComponent<Image>().sprite = actions[4];
    }

    public void ChangePressed()
    {
        StartCoroutine(PressButton(actions[3], actions[0], actionUI));
    }

    public void RunPressed()
    {
        StartCoroutine(PressButton(actions[2], actions[0], actionUI));
    }

    public void OpenSkillMenu()
    {
        skillSelectUI.SetActive(true);
    }

    public void CloseSkillMenu()
    {
        skillSelectUI.SetActive(false);
    }

    private IEnumerator PressButton(Sprite pressed, Sprite baseSp, GameObject defaultgo)
    {
        defaultgo.GetComponent<Image>().sprite = pressed;
        yield return new WaitForSeconds(0.3f);
        defaultgo.GetComponent<Image>().sprite = baseSp;
    }
}
