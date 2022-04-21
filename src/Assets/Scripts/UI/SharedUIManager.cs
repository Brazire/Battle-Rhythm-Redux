using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedUIManager : MonoBehaviour
{
    public static SharedUIManager sUIManager;
    [SerializeField] protected GameObject switchUi, atbUI, atbnumUi, actionUI, skillSelectUI, skill1, skill2, skill3;
    protected Animator switchAnim;
    [SerializeField] protected Sprite[] atbUiSprites = new Sprite[14];
    [SerializeField] protected Sprite[] actions = new Sprite[5];
    [SerializeField] protected GameObject hider;
    [SerializeField] protected GameObject camera;
    [SerializeField] protected GameObject defeat;
    [SerializeField] protected GameObject cleared;
    protected GameObject endMessage;
    protected bool facingLeft = false;
    protected float hideTimer = 0f;

    protected virtual void Awake()
    {
        sUIManager = this;
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

    //Used when switching between battle and rhythm mode
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

    //Sets unique skill icon to the skill menu
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

    protected void NumberUpdate(GameObject toUpdate, int number, int curNumber)
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

    //Victory fan fare
    public void StartVictory()
    {
        cleared.SetActive(true);
        endMessage = cleared;
        StartCoroutine(FanFare());
    }

    //Defeat fan fare
    public void StartDefeat()
    {
        defeat.SetActive(true);
        endMessage = defeat;
        StartCoroutine(FanFare());
    }

    //Routine to show the success message
    private IEnumerator FanFare()
    {
        while (endMessage.transform.localScale.x > 12)
        {
            endMessage.transform.localScale -= new Vector3(0.5f, 0.5f, 0f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        SharedBattleManager.sbManager.WeAllDie();
    }

    //Routine to delay button press
    private IEnumerator PressButton(Sprite pressed, Sprite baseSp, GameObject defaultgo)
    {
        defaultgo.GetComponent<Image>().sprite = pressed;
        yield return new WaitForSeconds(0.3f);
        defaultgo.GetComponent<Image>().sprite = baseSp;
    }
}
