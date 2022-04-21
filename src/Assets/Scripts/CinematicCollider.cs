using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CinematicCollider : MonoBehaviour
{
    [SerializeField]
    private int cinematicId;

    [SerializeField]
    private bool triggerOnce;

    private Cinematic actionList;
    private Dialog dialogList;
    private int actionIndex;
    private int talkIndex;

    private Dictionary<NpcEnum, npcObjectS> npcDict;
    private GameObject dialogBox;
    private float borderHeight;

    private float BORDER_SPEED = 0.01f;
    private float WALK_SPEED = 0.55f;
    private float RUN_SPEED = 0.85f;

    public struct npcObjectS
    {
        public GameObject gameObj;
        public Vector3 startingPos;
    }

    /// <summary>
    /// Verify if the cinematic should be enabled or not, load the action and text.
    /// </summary>
    private void Start()
    {
        if (PermManager.pManager.cinematicAlreadyPlayed.Contains(cinematicId))
        {
            gameObject.SetActive(false);
            return;
        }
        actionList = DialogReader.ActionsDict[cinematicId];
        dialogList = DialogReader.DialoguesDict[actionList.dialogId];

        npcDict = new Dictionary<NpcEnum, npcObjectS>(); 
        foreach (Transform child in GameObject.Find("NPC").transform)
        {
            npcDict[child.GetComponent<NPCCollider>().NpcId] = new npcObjectS 
            { 
                gameObj = child.gameObject, 
                startingPos = child.position
            };
        }
    }

    /// <summary>
    /// This method is the start of a cinematic. It set the index to 0, keep the current 
    /// position of npc, enable and setup the DialogBox and make the black border apprear.
    /// </summary>
    /// <param name="col">Default Collider2D</param>
    void OnTriggerEnter2D(Collider2D col)
    {
        actionIndex = 0;
        talkIndex = 0;

        GameObject.Find("Canvas").GetComponent<ToggleMenu>().ToggleDialog(true);
        borderHeight = GameObject.Find("BottomBar").GetComponent<RectTransform>().rect.height * 0.98f;

        dialogBox = GameObject.Find("DialogBoxP");
        dialogBox.GetComponent<RectTransform>().Translate(new Vector3(0, borderHeight, 0), Space.Self);
        dialogBox.SetActive(false);

        StartCoroutine(DisplayBlackBorder());
    }

    /// <summary>
    /// Coroutine that make the black border show up on screen.
    /// </summary>
    private IEnumerator DisplayBlackBorder()
    {
        var border = GameObject.Find("Blackbar").GetComponent<RectTransform>().localScale;
        for(float i = 1.3f; i >= 1f; i -= 0.01f)
        {
            border.y = i;
            GameObject.Find("Blackbar").GetComponent<RectTransform>().localScale = border;
            yield return new WaitForSeconds(BORDER_SPEED);
        }

        ExecuteAction(actionList.actions[actionIndex]);
    }

    /// <summary>
    /// Start a coroutine depending if the action is an action or a text to display.
    /// </summary>
    /// <param name="action"></param>
    private void ExecuteAction(Action action)
    {
        if (action.isAction)
            StartCoroutine(Action());
        else
            StartCoroutine(Dialog());
    }

    /// <summary>
    /// Either launch the next action or finish the cinematic by making the black border disapear.
    /// </summary>
    private void NextAction()
    {
        actionIndex++;
        if (actionIndex < actionList.actions.Count)
            ExecuteAction(actionList.actions[actionIndex]);
        else
            StartCoroutine(RemoveBlackBorder());
    }

    /// <summary>
    /// Coroutine that make the player or an NPC walk or run somewhere.
    /// </summary>
    private IEnumerator Action()
    {
        var action = actionList.actions[actionIndex];
        Vector3 destPosition = action.position;
        var speed = action.isWalking ? WALK_SPEED : RUN_SPEED;

        if (action.npcId != NpcEnum.None)
        {
            var npc = npcDict[action.npcId].gameObj;
            npc.GetComponent<NPCCollider>().canWalk = false;
            while (npc.transform.position != destPosition)
            {
                npc.transform.position = Vector3.MoveTowards(npc.transform.position, destPosition, speed);
                yield return null;
            }
        }
        else
        {
            var player = GameObject.Find("Player");
            while (player.transform.position != destPosition)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, destPosition, speed);
                player.GetComponent<PlayerMovement>().MakePlayerMove(destPosition - player.transform.position);
                yield return null;
            }
            player.GetComponent<PlayerMovement>().MakePlayerMove(new Vector3(0, 0, 0));
        }

        NextAction();
    }

    /// <summary>
    /// Coroutine that display text in the dialogBox, wait until the player click on the box.
    /// </summary>
    private IEnumerator Dialog()
    {
        dialogBox.SetActive(true);

        var dialogBoxC = GameObject.Find("DialogP").GetComponent<DialogBox>();
        dialogBoxC.DisplayCinematicText(dialogList.talks[talkIndex]);
        yield return new WaitUntil(() => dialogBoxC.CurrentStatus == DialogBox.Status.CinematicNext);

        dialogBox.SetActive(false);
        talkIndex++;
        NextAction();
    }

    /// <summary>
    /// Coroutine that make the black border disapear and reset the DialogBox and the npc position. 
    /// Disable the gameObject if supposed to execute only once.
    /// </summary>
    private IEnumerator RemoveBlackBorder()
    {
        var border = GameObject.Find("Blackbar").GetComponent<RectTransform>().localScale;
        for (float i = 1f; i < 1.3f; i += 0.01f)
        {
            border.y = i;
            GameObject.Find("Blackbar").GetComponent<RectTransform>().localScale = border;
            yield return new WaitForSeconds(BORDER_SPEED);
        }

        foreach(var qq in npcDict.Where(npc => !npcDict[npc.Key].gameObj.GetComponent<NPCCollider>().canWalk))
        {
            qq.Value.gameObj.transform.position = qq.Value.startingPos;
            qq.Value.gameObj.GetComponent<NPCCollider>().canWalk = true;
        }

        dialogBox.SetActive(true);
        dialogBox.GetComponent<RectTransform>().Translate(new Vector3(0, -borderHeight, 0), Space.Self);
        GameObject.Find("Canvas").GetComponent<ToggleMenu>().ToggleDialog(false);

        if (triggerOnce)
        {
            PermManager.pManager.cinematicAlreadyPlayed.Add(cinematicId);
            gameObject.SetActive(false);
        }
    }
}
