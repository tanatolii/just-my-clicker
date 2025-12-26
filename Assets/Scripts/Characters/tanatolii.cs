using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class tanatolii : MonoBehaviour
{
    public GameObject tanatoliiOnScene;
    [SerializeField] private GameObject tanatoliiPanel;
    public bool PanelWork;
    public List<faces> emotions = new List<faces>();
    public string CurrentEmotion = "sleep";
    public float EmotionsSpeed = 1.5f;
    void Start()
    {
        StartCoroutine(changing());
        TurnOnTheBackGround(PanelWork);
    }
    private IEnumerator changing()
    {
        for (int i = 5; i != 0; i--) {
            tanatoliiOnScene.GetComponent<Image>().sprite = emotions.FirstOrDefault(a => a.title == CurrentEmotion).face1;
            yield return new WaitForSeconds(EmotionsSpeed/5);
        }
        tanatoliiOnScene.GetComponent<Image>().sprite = emotions.FirstOrDefault(a => a.title == CurrentEmotion).face2;
        yield return new WaitForSeconds(EmotionsSpeed);
        StartCoroutine(changing());
    }
    [Serializable]
    public class faces
    {
        public string title;
        public Sprite face1;
        public Sprite face2;
    }
    public void TurnOnTheBackGround(bool state)
    {
        tanatoliiPanel.SetActive(state);
        Debug.Log("idfjuwcghj");
        PanelWork = state;
    }
    public void Appear(float speed)
    {
        DOTween.To(
            () => tanatoliiOnScene.GetComponent<Image>().color,           // Получить текущее значение
            x => tanatoliiOnScene.GetComponent<Image>().color = x,        // Установить новое значение
            new Color(1,1,1,1),                           // Конечное значение
            speed                             // Длительность в секундах
        ).SetEase(Ease.InOutCubic); 
    }
}