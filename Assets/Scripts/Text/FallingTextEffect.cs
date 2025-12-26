using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using NaughtyAttributes;

public class FallingTextEffect : MonoBehaviour
{
    [Header("Настройки")]
    public float typingSpeed = 0.05f;
    [Range(0.1f, 0.5f)] public float startDelayRange = 0.1f;
    public float fallSpeed = 50f;
    public float fadeSpeed = 2f;
    public AnimationCurve fallCurve;

    private TMP_Text textComponent;
    private bool isTyping = true;
    private Coroutine effectCoroutine;
    private List<Vector3[]> originalVertices = new List<Vector3[]>();
    public bool work;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        fallCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    }
    [Button]
    public void Disappear(string text)
    {
        GetComponent<TextAnimator_TMP>().animationLoop = AnimationLoop.Script;
        RestartEffect(textComponent.text);
    }
    public void Reset()
    {
        GetComponent<TextAnimator_TMP>().animationLoop = AnimationLoop.Update;
    }

    IEnumerator TypeAndFall(string fullText)
    {
        originalVertices.Clear();
        work = true;
        textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = textComponent.textInfo;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            Vector3[] verts = new Vector3[4];
            for (int j = 0; j < 4; j++)
            {
                verts[j] = textInfo.meshInfo[materialIndex].vertices[vertexIndex + j];
            }
            originalVertices.Add(verts);
        }

        List<Coroutine> fallCoroutines = new List<Coroutine>();

        for (int charIndex = 0; charIndex < textInfo.characterCount; charIndex++)
        {
            if (!textInfo.characterInfo[charIndex].isVisible) continue;

            float randomDelay = Random.Range(0, startDelayRange);
            yield return new WaitForSeconds(randomDelay);
            fallCoroutines.Add(StartCoroutine(FallSingleCharacter(charIndex, randomDelay)));
        }
        // Ждем завершения всех корутин
        foreach (var coroutine in fallCoroutines)
        {
            yield return coroutine;
        }
        work = false;
    }
    IEnumerator FallSingleCharacter(int charIndex, float startDelay)
    {
        TMP_TextInfo textInfo = textComponent.textInfo;
        textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

        if (!textInfo.characterInfo[charIndex].isVisible || charIndex >= textInfo.characterCount) yield break;

        int materialIndex = textInfo.characterInfo[charIndex].materialReferenceIndex;
        int vertexIndex = textInfo.characterInfo[charIndex].vertexIndex;
        Vector3[] charVertices = new Vector3[4];
        Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

        Vector3[] savedVerts = originalVertices[charIndex];
        for (int j = 0; j < 4; j++)
        {
            charVertices[j] = savedVerts[j];
        }
        float progress = 0f;
        float fadeValue = 1f;

        while (progress < 1f)
        {

            float fallProgress = fallCurve.Evaluate(progress);

            for (int j = 0; j < 4; j++)
            {
                charVertices[j].y = originalVertices[charIndex][j].y - (fallProgress * fallSpeed);
                textInfo.meshInfo[materialIndex].vertices[vertexIndex + j] = charVertices[j];
                textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            }

            fadeValue -= fadeSpeed * Time.deltaTime;
            byte alpha = (byte)(Mathf.Clamp01(fadeValue) * 255);
            for (int j = 0; j < 4; j++)
            {
                vertexColors[vertexIndex + j].a = alpha;
            }
            progress += Time.deltaTime * fadeSpeed;
            textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            yield return null;
        }
    }
    public void RestartEffect(string newText)
    {
        if (effectCoroutine != null) StopCoroutine(effectCoroutine);
        effectCoroutine = StartCoroutine(TypeAndFall(newText));
    }
}