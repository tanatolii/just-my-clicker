using UnityEngine;

public class a : MonoBehaviour
{
    public DialogueController controller;
    public DialogueInput dialogueInput;
    public void test()
    {
        dialogueInput.BlockPointerUpOneFrame();
        controller.StartDialogue("StartSceneMeeting", 1);
    }
}
