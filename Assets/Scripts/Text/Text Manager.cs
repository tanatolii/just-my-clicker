using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public bool conversation = false;
    [SerializeField] private Button Option1;
    [SerializeField] private Button Option2;
    [SerializeField] private Button Option3;
    [SerializeField] private Button MainButton;
    [SerializeField] private TextMeshProUGUI textArea;
    [SerializeField] private Dialogues dialoguesManager;
    [SerializeField] private FallingTextEffect fallingTextManager;
    [SerializeField] private int NextLine = 1;
    [SerializeField] private int CurrentDialogueId;
    [SerializeField] private Line currentLine;
    [SerializeField] private Characters characters;
    void Start()
    {
        Awaking();
    }
    public void Awaking()
    {
        HideCoice();
        textArea.text = " ";
    }
    public void Speak(int DialogueId)
    {
        if (!fallingTextManager.work) StartCoroutine(speaking(DialogueId, false));
    }
    private IEnumerator speaking(int DialogueId, bool cuuum)
    {
        if (cuuum == false)
        {
            cuuum = true;
            fallingTextManager.Disappear(currentLine.text);
        }
        if (fallingTextManager.work == false)
        {
            if (NextLine == 0)
            {
                EndDialogue();
            }
            currentLine = dialoguesManager.StartDialogue(DialogueId, NextLine);
            if (!conversation)
            {
                conversation = true;
                characters.ShowHideCharacter(currentLine.character, true);
            }
            CurrentDialogueId = DialogueId;
            fallingTextManager.Reset();

            // ______________________________________________________

            if (currentLine.id != 0) characters.SetEmotion(currentLine.character, currentLine.face);

            // ______________________________________________________
            
            textArea.text = currentLine.text;
            NextLine = currentLine.nextId;

            // ______________________________________________________

            if (currentLine.choices.Count != 0)
            {
                ShowCoice();

                MainButton.gameObject.SetActive(false);

                Option1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLine.choices[0].text;
                Option2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLine.choices[1].text;
                Option3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLine.choices[2].text;
            }

            // ______________________________________________________

            if (currentLine.skip) Speak(NextLine);

            // ______________________________________________________
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(speaking(DialogueId, cuuum));
        }
    }
    public void Choice(int num)
    {
        HideCoice();

        NextLine = currentLine.choices[num].nextId;

        Speak(CurrentDialogueId);
        MainButton.gameObject.SetActive(true);
    }



    private void ShowCoice()
    {
        Option1.gameObject.SetActive(true);
        Option2.gameObject.SetActive(true);
        Option3.gameObject.SetActive(true);
    }
    private void HideCoice()
    {
        Option1.gameObject.SetActive(false);
        Option2.gameObject.SetActive(false);
        Option3.gameObject.SetActive(false);
    }
    public void EndDialogue()
    {
        conversation = false;
        characters.ShowHideCharacter(currentLine.character, false);
        StopCoroutine(speaking(CurrentDialogueId, false));
        HideCoice();
        MainButton.gameObject.SetActive(false);
        textArea.gameObject.SetActive(false);
    }
}
