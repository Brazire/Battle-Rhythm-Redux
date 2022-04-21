using System.Collections.Generic;
using UnityEngine;

public class DialogReader : MonoBehaviour
{
    [SerializeField]
    private TextAsset Conversation;

    [SerializeField]
    private TextAsset Cinematic;

    public static Dictionary<int, Dialog> DialoguesDict { get; private set; }
    public static Dictionary<int, Cinematic> ActionsDict { get; private set; }

    /// <summary>
    /// Load 2 .json file, one for the cinematic and the other for the dialog.
    /// </summary>
    void Awake()
    {
        DialoguesDict = new Dictionary<int, Dialog>();
        ActionsDict = new Dictionary<int, Cinematic>();

        var dialogs = JsonUtility.FromJson<DialogList>(Conversation.text);
        foreach(var dialog in dialogs.dialogs)
            DialoguesDict.Add(dialog.id, dialog);

        var actions = JsonUtility.FromJson<CinematicList>(Cinematic.text);
        foreach (var action in actions.lists)
            ActionsDict.Add(action.id, action);
    }
}
