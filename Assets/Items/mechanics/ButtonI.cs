using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonI : Item
{
    [Header("Refs")]
    [SerializeField] private Wallet wallet;
    [SerializeField] private ClickI click;
    [SerializeField] private Image buttonImage;

    public Sprite PressedUp;
    public Sprite PressedDown;
    public UnityEvent ButtonPressed;
    public float Delay;
    public bool IsPressed;
    private int[] costs = new int[] { 50, 100, 175, 250, 375, 600, 1000, 2500, 5000, 10000 };
    public void Press()
    {
        if (IsPressed) return;

        ButtonPressed?.Invoke();
        wallet.ScoreChange(click.Power);
        StartCoroutine(WaitPressAnim());
    }
    private IEnumerator WaitPressAnim()
    {
        buttonImage.sprite = PressedDown;
        IsPressed = true;
        yield return new WaitForSeconds(Delay);
        buttonImage.sprite = PressedUp;
        IsPressed = false;
    }
    public override bool TryBuy(Wallet wallet)
    {
        if (level >= 10) return false;
        if (!base.TryBuy(wallet)) return false;

        LevelUp();
        bonus = Delay;
        return true;
    }

    private void LevelUp()
    {
        Delay = 0.9f - 0.08f * level;
        cost = costs[level - 1];
    }
}
