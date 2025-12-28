using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public int level;
    public string itemName;
    public string description;
    public bool isBought;
    public float cost;
    public Sprite sprite;
    public int id;
    public float bonus = 0;
    public UnityEvent EventAfterBuy;

    public virtual bool TryBuy(Wallet wallet)
    {
        if (wallet.Score < cost) return false;

        level += 1;
        wallet.ScoreChange(-cost);
        EventAfterBuy?.Invoke();
        return true;
    }

    public virtual void Do()
    {

    }
}
