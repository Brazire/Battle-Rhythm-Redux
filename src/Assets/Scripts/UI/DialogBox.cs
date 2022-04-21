using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField]
    private GameObject Text;

    [SerializeField]
    private GameObject ChoiceLayout;

    [SerializeField]
    private GameObject NameP;

    private string TextToDisplay;
    private Coroutine writeCo;
    public float textSpeed = 0.06f;

    private int index;
    private List<Talk> talks;
    private List<Choice> choices;
    public Status CurrentStatus { get; private set; }
    private NPCCollider currentNpc;

    public static bool isFree = true;
    private bool isCinematic = false;

    private readonly Dictionary<string, string> regexDict = new Dictionary<string, string>
        {
            { @"\*", "b" },
            { @"\|", "color=green" },
            { "#", "color=blue" },
            { "¬", "color=red" },
            { "@", "i" }
        };


    public enum Status
    {
        Writing,
        Completed,
        Starting,
        CinematicNext
    }

    /// <summary>
    /// Set the value at the starting value.
    /// </summary>
    private void OnEnable()
    {
        CurrentStatus = Status.Starting;
        index = 0;
        isFree = false;
        isCinematic = false;
    }

    /// <summary>
    /// Tell that the dialogBox is not used anymore.
    /// </summary>
    private void OnDisable() => isFree = true;

    /// <summary>
    /// Method called when the player click in the dialogBox, either to display the next text or to skip the letter-by-letter display. 
    /// If it's from a cinematic, it changes the status to give control back to the cinematic, 
    /// if it reach the end of the dialog, either display choices if exists or close the dialogBox.
    /// </summary>
    public void NextText()
    {
        switch (CurrentStatus)
        {
            // The user clicked next when the text is still being displayed
            case Status.Writing:
                StopCoroutine(writeCo);
                Text.GetComponent<Text>().text = Convert(TextToDisplay);
                CurrentStatus = Status.Completed;
                break;

            // The user clicked when the text was completely displayed
            case Status.Completed:
                index++;
                if (isCinematic)
                    CurrentStatus = Status.CinematicNext;
                else if (index < talks.Count)
                {
                    writeCo = StartCoroutine(PrintText());
                    CurrentStatus = Status.Writing;
                }
                else if (choices.Count > 0)
                    DisplayChoice();
                else
                {
                    GameObject.Find("Canvas").GetComponent<ToggleMenu>().ToggleDialog(false);
                    if (currentNpc.isMerchant)
                        GameObject.Find("Canvas").GetComponent<ToggleMenu>().OpenShop();
                }
                break;
        }
    }

    /// <summary>
    /// Method that display the choice a player can make at the end of a dialog. It remove the old choices before 
    /// creating the new one. Select the first one by default. When clicking a choice, it either start a new dialogs or close.
    /// </summary>
    private void DisplayChoice()
    {
        foreach (Transform child in ChoiceLayout.transform)
            Destroy(child.gameObject);

        var isFirst = true;
        foreach(var choice in choices)
        {
            var newButton = DefaultControls.CreateButton(new DefaultControls.Resources());
            newButton.GetComponent<RectTransform>().sizeDelta = new Vector2 (0, NameP.GetComponent<RectTransform>().rect.height * 0.8f);
            newButton.transform.parent = ChoiceLayout.transform;

            newButton.GetComponentInChildren<Text>().text = choice.text;
            newButton.GetComponentInChildren<Text>().fontSize = 20;

            if (choice.branchingId == 0)
                newButton.GetComponentInChildren<Button>().onClick.AddListener(() => GameObject.Find("Canvas").GetComponent<ToggleMenu>().ToggleDialog(false));
            else
                newButton.GetComponentInChildren<Button>().onClick.AddListener(() =>
                {
                    OnEnable();
                    DisplayTexts(DialogReader.DialoguesDict[choice.branchingId], currentNpc);
                }
                );

            if (isFirst)
            {
                EventSystem.current.SetSelectedGameObject(newButton);
                isFirst = false;
            }
        }

        ChoiceLayout.SetActive(true);
    }

    /// <summary>
    /// Method that start a conversation with a npc.
    /// </summary>
    /// <param name="dialog">The dialog that contains the list of text to display and the choices to choose from.</param>
    /// <param name="npc">The npc object the player is talking to.</param>
    public void DisplayTexts(Dialog dialog, NPCCollider npc)
    {
        currentNpc = npc;
        if (dialog.id == 0)
        {
            talks = new List<Talk>() { dialog.talks[Random.Range(0, dialog.talks.Count)] };
            talks[0].npcId = currentNpc.NpcId;
            choices = new List<Choice>();
        }
        else
        {
            talks = dialog.talks;
            choices = dialog.choices;
        }

        ChoiceLayout.SetActive(false);
        writeCo = StartCoroutine(PrintText());
        CurrentStatus = Status.Writing;
        EventSystem.current.SetSelectedGameObject(Text.transform.parent.gameObject);
    }

    /// <summary>
    /// The method used by the cinematic to display one text.
    /// </summary>
    /// <param name="talk">The talk object contains the text to display.</param>
    public void DisplayCinematicText(Talk talk)
    {
        talks = new List<Talk>() { talk };
        choices = new List<Choice>();
        index = 0;
        isCinematic = true;

        ChoiceLayout.SetActive(false);
        writeCo = StartCoroutine(PrintText());

        CurrentStatus = Status.Writing;
        EventSystem.current.SetSelectedGameObject(Text.transform.parent.gameObject); 
    }

    /// <summary>
    /// Coroutine that display the text letter-by-letter.
    /// </summary>
    private IEnumerator PrintText()
    {
        if (talks[index].npcId == NpcEnum.None)
            NameP.SetActive(false);
        else
        {
            NameP.SetActive(true);
            NameP.GetComponentInChildren<Text>().text = talks[index].npcId.ToString();
        }

        TextToDisplay = ReplaceTextWithVar(talks[index].text);
        for(int i = 1; i <= TextToDisplay.Length; i++)
        {
            Text.GetComponent<Text>().text = Convert(TextToDisplay.Substring(0, i));
            yield return new WaitForSeconds(textSpeed);
        }

        if (index == talks.Count - 1 && choices.Count > 0)
            DisplayChoice();

        CurrentStatus = Status.Completed;
    }

    /// <summary>
    /// Method used to add variable text in a string.
    /// </summary>
    /// <param name="text">The string to replace the var with the actual value.</param>
    /// <returns></returns>
    public static string ReplaceTextWithVar(string text)
    {
        Regex rgx = new Regex("%[A-Za-z]+%");
        var matches = rgx.Matches(text);
        if (matches.Count == 0)
            return text;

        foreach (Match match in matches)
            text = text.Replace(match.Value, PermManager.pManager.varDict[match.Value]);

        return text;
    }

    /// <summary>
    /// Method used to replace symbol from a dict with html tags to display color or effect on the text.
    /// </summary>
    /// <param name="sentence">The string to replace symbol with html things <b>like that</b></param>
    /// <returns></returns>
    private string Convert(string sentence)
    {
        foreach (var regex in regexDict)
        {
            var rgx = new Regex(regex.Key);
            var matches = rgx.Matches(sentence);
            if (matches.Count == 0)
                continue;

            for (int i = matches.Count - 1; i >= 0; i--)
            {
                sentence = sentence.Remove(matches[i].Index, matches[i].Length);
                if (i % 2 == 0)
                    sentence = sentence.Insert(matches[i].Index, "<" + regex.Value + ">");
                else
                    sentence = sentence.Insert(matches[i].Index, "</" + regex.Value.Split('=')[0] + ">");
            }

            if (matches.Count % 2 == 1)
                sentence += "</" + regex.Value.Split('=')[0] + ">";
        }

        return sentence;
    }
}
