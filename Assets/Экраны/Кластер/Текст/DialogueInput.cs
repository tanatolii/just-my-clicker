using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private DialogueController controller;
    [SerializeField] private DialogueView view;
    [SerializeField] private float holdMsToSkip = 250f;
    [SerializeField] private bool work = false;

    private bool isDown;
    private bool skipped;
    private float downTime;
    private bool blockUp;

    private void Update()
    {
        if (!isDown || skipped) return;
        if (controller.CurrentState != DialogueController.State.Typing) return;

        if ((Time.unscaledTime - downTime) * 1000f >= holdMsToSkip)
        {
            skipped = true;
            view.SkipFrontInstant();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        skipped = false;
        downTime = Time.unscaledTime;
    }

    public void BlockPointerUpOneFrame()
    {
        blockUp = true;
        StartCoroutine(UnblockNextFrame());
    }

    private IEnumerator UnblockNextFrame()
    {
        yield return new WaitForSeconds(0.1f); // 1 кадр
        blockUp = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!work) return;
        //if (EventSystem.current.IsPointerOverGameObject()) return;
        if (blockUp) return;   // <-- ключ
        isDown = false;
        if (skipped) return;
        if (controller.CurrentState == DialogueController.State.WaitingTap)
            controller.Advance();
    }
}
