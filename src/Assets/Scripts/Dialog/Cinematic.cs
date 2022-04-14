using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CinematicList
{
    public List<Cinematic> lists;
}

[Serializable]
public class Cinematic
{
    public int id;
    public int dialogId;
    public List<Action> actions;
}

[Serializable]
public class Action
{
    public bool isAction;
    public bool isWalking;
    public NpcEnum npcId;
    public Vector2 position;
}
