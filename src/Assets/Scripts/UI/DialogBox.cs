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

    private void OnEnable()
    {
        CurrentStatus = Status.Starting;
        index = 0;
        isFree = false;
        isCinematic = false;
    }

    private void OnDisable() => isFree = true;

    // Methode called when the player click in the dialogBox
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

    private void DisplayChoice()
    {
        foreach (Transform child in ChoiceLayout.transform)
            Destroy(child.gameObject);

        var isFirst = true;
        foreach(var choice in choices)
        {
            var newButton = DefaultControls.CreateButton(new DefaultControls.Resources());
            newButton.transform.parent = ChoiceLayout.transform;
            newButton.GetComponentInChildren<Text>().text = choice.text;

            if(choice.branchingId == 0)
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

    // Methode to use at the start of a conversation.
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

    // Coroutine that display the text letter-by-letter
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
