using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] List<ItemShop> items = new List<ItemShop>();

    public void UpdateShop()
    {
        foreach (ItemShop item in items)
        {
            item.itemShopUpdate();
        }
    }
}
