using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    [SerializeField] private SpriteRenderer itemImage;
    [SerializeField] private TextMeshProUGUI itemTitle;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Item item;

    void Start()
    {
        itemShopUpdate();
    }
    public void itemShopUpdate()
    {
        itemImage.sprite = item.sprite;
        itemTitle.text = item.itemName;
        itemDescription.text = "стоимость: " + item.cost + "\nбонус: " + item.bonus;
    }
}
