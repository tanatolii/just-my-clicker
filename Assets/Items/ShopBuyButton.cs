using UnityEngine;

public class ShopBuyButton : MonoBehaviour
{
    [SerializeField] private Wallet wallet;
    [SerializeField] private Item item;

    public void Buy()
    {
        if (wallet == null || item == null) return;
        item.TryBuy(wallet);
    }
}
