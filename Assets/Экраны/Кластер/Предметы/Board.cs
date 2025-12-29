using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Board : Item
{
    public List<TextMeshProUGUI> textAreas = new List<TextMeshProUGUI>();
    public void ScoreBoardsUpdate(float score)
    {
        foreach (TextMeshProUGUI area in textAreas)
        {
            area.text = Math.Round(score, 1).ToString();
        }
    }
}
