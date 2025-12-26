using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StartSceneBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent unityAction;
    [SerializeField] private int seconds;
    void Start()
    {
        StartCoroutine(wait(seconds));
    }
    private IEnumerator wait(int seconds) {
        yield return new WaitForSeconds(1);
        seconds--;
        if (seconds != 0) {
            StartCoroutine(wait(seconds));
        } else
        {
            unityAction?.Invoke();
        }
    }
}
