using System;
using System.Collections.Generic;

[Serializable]
public class DialogueFile
{
    public List<DialogueLine> dialogue;
}

[Serializable]
public class DialogueLine
{
    public int id;
    public string text;
    public string character;
    public string face;
    public List<DialogueChoice> choices;
    public int nextId;
    public float money;
    public bool skip;
}

[Serializable]
public class DialogueChoice
{
    public string text;
    public int nextId;
    public float money;
}
