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

public enum NpcEnum
{
    None,
    Tree,
    Red,
    Purple,
    Merchant,
    Orange
}