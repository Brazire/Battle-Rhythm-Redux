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

    void Update()
    {
        if (walkingPoints != null && walkingPoints.Count > 1 && canWalk)
        {
            transform.position = Vector3.MoveTowards(transform.position, walkingPoints[destinationCoordinate], 0.5f); 
            if (transform.position == walkingPoints[destinationCoordinate])
                destinationCoordinate = (destinationCoordinate + 1) % walkingPoints.Count;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        canWalk = false;
        rControls.World.Interraction.Enable();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        canWalk = true;
        rControls.World.Interraction.Disable();
    }

    private void StartConversation(InputAction.CallbackContext obj)
    {
        if (DialogBox.isFree && GameObject.Find("Canvas").GetComponent<ToggleMenu>().IsDialogPossible)
        {
            GameObject.Find("Canvas").GetComponent<ToggleMenu>().ToggleDialog(true);
            GameObject.Find("DialogP").GetComponent<DialogBox>().DisplayTexts(DialogReader.DialoguesDict[dialogId], this);
        }
    }
}
