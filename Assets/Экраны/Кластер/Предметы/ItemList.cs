using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();

    public Item GetByIndex(int index)
    {
        if (index < 0 || index >= items.Count) return null;
        return items[index];
    }

    public int Count => items.Count;
}
