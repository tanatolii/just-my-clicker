using DG.Tweening;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToGetDistance;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float screenMultiplier = 1f;
    [SerializeField] public Material invertMaterial;
    private float standartRevolution;
    private Vector3 difference;

    void Start()
    {
        difference = transform.position - ObjectToGetDistance.transform.position;
        Application.targetFrameRate = -1;
        standartRevolution = 1080f / 1920f;
        screenMultiplier = (float)Screen.width / Screen.height;
        screenMultiplier = standartRevolution / screenMultiplier;
        GetComponent<Camera>().fieldOfView *= screenMultiplier;
        
        
    }
    public void MoveTo(GameObject Place)
    {
        Vector3 place = Place.transform.position + difference;
        transform.DOMove(place, speed).SetEase(Ease.InOutCubic);
    }
    public void MoveToWithCamera(GameObject cam)
    {
        transform.DOMove(cam.transform.position, speed).SetEase(Ease.InOutCubic).SetUpdate(UpdateType.Fixed);
        transform.DORotate(cam.transform.rotation.eulerAngles, speed).SetEase(Ease.InOutCubic);
         DOTween.To(
            () => GetComponent<Camera>().fieldOfView,           // Получить текущее значение
            x => GetComponent<Camera>().fieldOfView = x,        // Установить новое значение
            cam.GetComponent<Camera>().fieldOfView * screenMultiplier,                           // Конечное значение
            speed                             // Длительность в секундах
        ).SetEase(Ease.InOutCubic); 
    }
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, invertMaterial);
    }
}
