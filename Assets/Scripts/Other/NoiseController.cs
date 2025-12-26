using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NoiseController : MonoBehaviour
{
    [SerializeField] private float alpha = 1f;
    [SerializeField] private float noiseIntensity;
    [SerializeField] private GameObject one;
    [SerializeField] private GameObject two;
    void Update()
    {
        GetComponent<Image>().material.SetColor("_Color", new Color(1, 1, 1, alpha));
        GetComponent<Image>().material.SetFloat("_NoiseAmount", noiseIntensity);
        if (alpha == 0)
        {
            one.SetActive(false);
            two.SetActive(false);
        }
    }
}