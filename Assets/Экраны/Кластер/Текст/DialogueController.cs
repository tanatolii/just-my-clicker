using System;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public enum State { Idle, Typing, WaitingTap, Choosing }

    [SerializeField] private DialogueDatabase db;

    public State CurrentState { get; private set; } = State.Idle;

    public string CurrentKey { get; private set; }
    public int CurrentLineId { get; private set; }

    public event Action<DialogueLine> OnLine;
    public event Action OnEnd;

    public void StartDialogue(string key, int startLineId = 1)
    {
        CurrentKey = key;
        CurrentLineId = startLineId;
        EmitLine();
    }

    public void NotifyTextShown()
    {
        if (CurrentState != State.Typing) return;

        var line = db.GetLine(CurrentKey, CurrentLineId);
        if (line == null) { EndDialogue(); return; }

        CurrentState = (line.choices != null && line.choices.Count > 0)
            ? State.Choosing
            : State.WaitingTap;
    }

    public void Advance()
    {
        if (CurrentState != State.WaitingTap) return;

        var line = db.GetLine(CurrentKey, CurrentLineId);
        if (line == null || line.nextId == 0) { EndDialogue(); return; }

        CurrentLineId = line.nextId;
        EmitLine();
    }

    public void Choose(int choiceIndex)
    {
        if (CurrentState != State.Choosing) return;

        var line = db.GetLine(CurrentKey, CurrentLineId);
        if (line?.choices == null) return;
        if (choiceIndex < 0 || choiceIndex >= line.choices.Count) return;

        int next = line.choices[choiceIndex].nextId;
        if (next == 0) { EndDialogue(); return; }

        CurrentLineId = next;
        EmitLine();
    }

    private void EmitLine()
    {
        var line = db.GetLine(CurrentKey, CurrentLineId);
        if (line == null) { EndDialogue(); return; }

        CurrentState = State.Typing;
        OnLine?.Invoke(line);
    }

    private void EndDialogue()
    {
        CurrentState = State.Idle;
        OnEnd?.Invoke();
    }
}
