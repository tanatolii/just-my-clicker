using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Febucci.UI;

public class DialogueView : MonoBehaviour
{
    [Header("Logic")]
    [SerializeField] private DialogueController controller;

    [Header("Characters")]
    [SerializeField] private Characters characters;

    [Header("UI")]
    [SerializeField] private CanvasGroup root;
    [SerializeField] private TMP_Text frontText;
    [SerializeField] private TMP_Text backText;

    [Header("Febucci")]
    [SerializeField] private TypewriterByCharacter frontTypewriter;
    [SerializeField] private TypewriterByCharacter backTypewriter;

    [Header("Choices")]
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private Button[] choiceButtons;

    private DialogueLine currentLine;

    private void Awake()
    {

        controller.OnLine += ShowLine;
        controller.OnEnd += HideAll;

        HideChoices();
        HideRoot();
    }

    private void ShowLine(DialogueLine line)
    {
        currentLine = line;
        ShowRoot();

        // Персонаж/эмоция
        if (!string.IsNullOrEmpty(line.character))
        {
            characters.ShowHideCharacter(line.character, true);
            if (!string.IsNullOrEmpty(line.face))
                characters.SetEmotion(line.character, line.face);
        }

        // 1) back получает старый front (если был)
        var prev = frontText.text;
        if (!string.IsNullOrWhiteSpace(prev))
        {
            backTypewriter.ShowText("{#fall inv=1}" + prev);
            //backText.text = "{#fall inv=1}" + prev;
            backTypewriter.SkipTypewriter();

            // 2) Запускаем исчезновение только если реально есть что исчезать
            backTypewriter.StartDisappearingText();
            backTypewriter.SkipTypewriter();
        }
        else backTypewriter.ShowText("");

        // 3) Новый текст на front
        frontTypewriter.ShowText(line.text ?? "");
        frontTypewriter.StartShowingText();

        // Choices
        if (line.choices != null && line.choices.Count > 0) ShowChoices(line);
        else HideChoices();
    }


    // Этот метод подключи в инспекторе к frontTypewriter -> Events -> On Text Showed() [image:793]
    public void OnFrontTextShown()
    {
        controller.NotifyTextShown();

        if (currentLine != null && currentLine.skip)
            controller.Advance();
    }

    public void OnChoice(int index) => controller.Choose(index);

    public void SkipFrontInstant()
    {
        frontTypewriter.SkipTypewriter();
        // Обычно после SkipTypewriter Febucci сам вызовет On Text Showed(), но даже если нет —
        // событие можно дёрнуть вручную (оставь закомментированным, пока не понадобится).
        // OnFrontTextShown();
    }

    private void ShowChoices(DialogueLine line)
    {
        choicesPanel.SetActive(true);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            bool on = i < line.choices.Count;
            choiceButtons[i].gameObject.SetActive(on);
            if (!on) continue;

            choiceButtons[i].GetComponentInChildren<TMP_Text>().text = line.choices[i].text;
        }
    }

    private void HideChoices()
    {
        if (choicesPanel != null) choicesPanel.SetActive(false);
        if (choiceButtons == null) return;
        for (int i = 0; i < choiceButtons.Length; i++)
            choiceButtons[i].gameObject.SetActive(false);
    }

    private void HideAll()
    {
        HideChoices();
        frontText.text = "";
        backText.text = "";
        HideRoot();

        if (currentLine != null && !string.IsNullOrEmpty(currentLine.character))
            characters.ShowHideCharacter(currentLine.character, false);
    }

    private void ShowRoot()
    {
        if (root == null) return;
        root.alpha = 1;
        root.blocksRaycasts = true;
        root.interactable = true;
    }

    private void HideRoot()
    {
        if (root == null) return;
        root.alpha = 0;
        root.blocksRaycasts = false;
        root.interactable = false;
    }
}
