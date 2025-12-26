using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonI : Item
{
    public Sprite PressedUp;
    public Sprite PressedDown;
    public UnityEvent ButtonPressed;
    public GameObject ObjectOnScene;
    public float Delay;
    public bool IsPressed;
    Wallet w;
    ClickI c;
    private int[] costs = new int[] { 50, 100, 175, 250, 375, 600, 1000, 2500, 5000, 10000 };
    void Start()
    {
        ItemList itemList = transform.parent.GetComponent<ItemList>();
        w = itemList.GetItem(4) as Wallet;
        c = itemList.GetItem(1) as ClickI;
    }
    public void Press()
    {
        if (!IsPressed)
        {
            ButtonPressed?.Invoke();
            w.ScoreChange(c.Power);
            StartCoroutine(wait());
        }
    }
    private IEnumerator wait()
    {
        ObjectOnScene.GetComponent<Image>().sprite = PressedDown;
        IsPressed = true;
        yield return new WaitForSeconds(Delay);
        ObjectOnScene.GetComponent<Image>().sprite = PressedUp;
        IsPressed = false;
    }
    public override void Buy(Wallet wallet)
    {
        if (level != 10)
        {
            base.Buy(wallet);
            levelUP();
            bonus = Delay;
        }
    }
    private void levelUP()
    {
        Delay = 0.9f - 0.08f * level;
        cost = costs[level - 1];
    }
}
