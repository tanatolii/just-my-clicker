using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    [SerializeField] private List<DialogueSegment> dialogues; // Список всех диалогов
    [SerializeField] private int currentDialogueID;    // Текущий ID диалога
    [SerializeField] private int currentLineID;
    [SerializeField] private int nextLineID = 1;

    void Start()
    {
        LoadDialogues();
    }
    [Button]
    public void LoadDialogues()
    {
        dialogues.Clear();
        currentDialogueID = 0;
        currentLineID = 0;
        TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>("Dialogues");
        foreach (TextAsset jf in jsonFiles)
        {
            DialogueList data = JsonUtility.FromJson<DialogueList>(jf.text);
            dialogues.Add(new DialogueSegment
            {
                id = Convert.ToInt32(data.dialogue[0].text[data.dialogue[0].text.Length - 1].ToString()),
                dialogue = data.dialogue
            });
        }
    }
    public Line StartDialogue(int dialogueID, int currId = 1)
    {
        Line current = dialogues.Find(d => d.id == dialogueID).dialogue.Find(d => d.id == currId);
        return current;
    }
}
[Serializable]
public class DialogueSegment
{
    public int id;
    public List<Line> dialogue;
}
[Serializable]
public class Line
{
    public int id;          // ID диалога
    public string text;     // Текст реплики
    public string character; // Персонаж
    public string face; // Эмоция персонажа
    public List<Choice> choices; // Варианты ответов
    public int nextId;      // ID следующего диалога
    public float money; // Сколько монет будет вычитаться
    public bool skip; // Автоматический пропуск текста после написания
}

[Serializable]
public class Choice
{
    public string text;    // Текст выбора
    public int nextId;     // Куда ведёт выбор
    public float money;
}

// Обёртка для списка диалогов (чтобы JsonUtility работал)
[Serializable]
public class DialogueList {
    public List<Line> dialogue;
}