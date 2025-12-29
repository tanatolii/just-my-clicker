using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    private readonly Dictionary<string, DialogueFile> cache = new();

    [SerializeField] private string resourcesFolder = "Dialogues"; // Resources/Dialogues

    private void Awake()
    {
        LoadAll();
    }

    public void LoadAll()
    {
        cache.Clear();
        var files = Resources.LoadAll<TextAsset>(resourcesFolder);
        foreach (var ta in files)
        {
            string json = ta.text;
            if (!string.IsNullOrEmpty(json) && json[0] == '\uFEFF')
                json = json.Substring(1);

            json = json.TrimStart();           // убираем пробелы/переводы строк перед "{"
            if (!json.StartsWith("{"))         // не JSON — пропускаем
                continue;

            var file = JsonUtility.FromJson<DialogueFile>(json);
            if (file == null || file.dialogue == null)
                continue;

            cache[ta.name] = file;
        }

    }

    public DialogueLine GetLine(string key, int id)
    {
        if (!cache.TryGetValue(key, out var file) || file.dialogue == null) return null;
        return file.dialogue.Find(l => l.id == id);
    }
}
