using System;
using UnityEngine;

public class ClickI : Item
{
    public float Power;
    public override void Buy(Wallet wallet)
    {
        if (level != 100)
        {
            base.Buy(wallet);
            if (wallet.Score >= cost) levelUP();
        }
        bonus = Power;
    }
    private void levelUP()
    {
        if (level <= 100)
        {
            Power = 1 + 0.1f * (float) Math.Pow(1.45923f, level - 1);
            cost += 10 * level;
        }
    }
}
