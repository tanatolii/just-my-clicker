using System;
using UnityEngine;

public class ClickI : Item
{
    public float Power;
    public override bool TryBuy(Wallet wallet)
    {
        if (level >= 100) return false;
        if (!base.TryBuy(wallet)) return false;

        LevelUp();
        bonus = Power;
        return true;
    }

    private void LevelUp()
    {
        Power = 1 + 0.1f * (float)Math.Pow(1.45923f, level - 1);
        cost += 10 * level; // цена следующего уровня
    }
}
