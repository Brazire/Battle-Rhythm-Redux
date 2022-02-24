using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager;
    [SerializeField]private GameObject switchUi, atbUI, atbnumUi, actionUI, skillSelectUI, skill1, skill2, skill3;
    private Animator switchAnim;
    [SerializeField]private Sprite[] atbUiSprites = new Sprite[14];
    [SerializeField]private Sprite[] actions = new Sprite[5];
    [SerializeField] private GameObject hider;
    [SerializeField] private GameObject camera;
    private bool facingLeft = false;
    private float hideTimer = 0f;

    private void Awake()
    {
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

    public void StartShake()
    {
        camera.GetComponent<CameraShaker>().StartShake();
    }

    public void UpdateATB(int number)
    {
        atbUI.GetComponent<Image>().sprite = atbUiSprites[number];
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
        atbnumUi.transform.GetChild(curNumber).gameObject.SetActive(false);
        atbnumUi.transform.GetChild(number).gameObject.SetActive(true);
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
