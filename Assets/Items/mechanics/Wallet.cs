using System;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : Item
{
    public float Score;
    public FloatEvent ScoreChanged;
    public void ScoreChange(float scoreToAdd)
    {
        Score += scoreToAdd;
        ScoreChanged?.Invoke(Score);
    }
}

[Serializable]
public class FloatEvent : UnityEvent<float> { }