using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCCollider : MonoBehaviour
{
    [SerializeField]
    private int dialogId;

    [SerializeField]
    private NpcEnum _npcId;
    public NpcEnum NpcId { get { return _npcId; } private set { _npcId = value; } }

    [SerializeField]
    private List<Vector3> walkingPoints;

    [HideInInspector]
    public bool canWalk = true;

    public bool isMerchant;
    private RhythmControls rControls;
    private int destinationCoordinate = 0;

    void Awake()
    {
        rControls = new RhythmControls();
        rControls.World.Interraction.performed += StartConversation;
    }

    /// <summary>
    /// If the npc have 2 or more point and they can walk (will stop walking when they got an action in a cinematic), 
    /// they will loop through those points to walk.
    /// </summary>
    void Update()
    {
        if (walkingPoints != null && walkingPoints.Count > 1 && canWalk)
        {
            transform.position = Vector3.MoveTowards(transform.position, walkingPoints[destinationCoordinate], 0.5f); 
            if (transform.position == walkingPoints[destinationCoordinate])
                destinationCoordinate = (destinationCoordinate + 1) % walkingPoints.Count;
        }
    }

    /// <summary>
    /// The player is in the zone to talk with this npc.
    /// </summary>
    /// <param name="col">Default Collider2D param.</param>
    void OnTriggerEnter2D(Collider2D col)
    {
        canWalk = false;
        rControls.World.Interraction.Enable();
    }

    /// <summary>
    /// The player got out of the zone to talk with this npc.
    /// </summary>
    /// <param name="col">Default Collider2D param.</param>
    void OnTriggerExit2D(Collider2D col)
    {
        canWalk = true;
        rControls.World.Interraction.Disable();
    }

    /// <summary>
    /// The method called to start a conversation with a npc. It will open the dialogBox and decide which dialog to display.
    /// </summary>
    /// <param name="obj"></param>
    private void StartConversation(InputAction.CallbackContext obj)
    {
        if (DialogBox.isFree && GameObject.Find("Canvas").GetComponent<ToggleMenu>().IsDialogPossible)
        {
            GameObject.Find("Canvas").GetComponent<ToggleMenu>().ToggleDialog(true);
            GameObject.Find("DialogP").GetComponent<DialogBox>().DisplayTexts(DialogReader.DialoguesDict[dialogId], this);
        }
    }
}
