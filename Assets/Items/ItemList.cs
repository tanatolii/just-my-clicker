using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();

    public Item GetItem(int id)
    {
        return items.Find(i => i.id == id);
    }
    public int GetLength()
    {
        return items.Count();
    }
}
