using System;
using System.Collections.Generic;

[Serializable]
public class DialogList
{
    public List<Dialog> dialogs;
}

[Serializable]
public class Dialog
{
    public int id;
    public List<Talk> talks;
    public List<Choice> choices;
}

[Serializable]
public class Talk
{
    public string text;
    public NpcEnum npcId;
}

[Serializable]
public class Choice
{
    public int branchingId;
    public string text;
}

/// <summary>
/// This enum contains the name of all the npc. Is used to give an id to the npc.
/// </summary>
public enum NpcEnum
{
    None,
    Tree,
    Red,
    Purple,
    Merchant,
    Orange
}