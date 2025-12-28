using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class NoteBookController : MonoBehaviour
{

    [SerializeField] private Page page1;
    [SerializeField] private Page page2;
    [SerializeField] private Claster claster;
    [SerializeField] private int pageNumber = 1;
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private TextMeshProUGUI leftPageNumber;
    [SerializeField] private TextMeshProUGUI rightPageNumber;
    private ItemList itemList;
    void Start()
    {
        itemList = claster.itemList;
        Refresh();
    }
    public void PageUp()
    {
        pageNumber++;
        Refresh();
    }
    public void PageDown()
    {
        pageNumber--;
        Refresh();
    }
    private void Refresh()
    {
        int leftIndex = (pageNumber - 1) * 2;

        var leftItem = itemList.GetByIndex(leftIndex);
        if (leftItem != null) { page1.ItemShow(); page1.UpdateItem(leftItem); }
        else { page1.ItemHide(); }

        var rightItem = itemList.GetByIndex(leftIndex + 1);
        if (rightItem != null) { page2.ItemShow(); page2.UpdateItem(rightItem); }
        else { page2.ItemHide(); }

        leftArrow.interactable = pageNumber > 1;
        rightArrow.interactable = itemList.Count > leftIndex + 2;

        leftPageNumber.text = (leftIndex + 1).ToString();
        rightPageNumber.text = (leftIndex + 2).ToString();
    }


    [Serializable]
    public class Page {
        public TextMeshProUGUI TitleArea;
        public TextMeshProUGUI DescriptionArea;
        public Image ItemImage;
        public int ID;

        public void UpdateItem(Item item)
        {
            if (item == null) { ItemHide(); return; }
            TitleArea.text = item.itemName;
            DescriptionArea.text = item.description;
            ItemImage.sprite = item.sprite;
            ID = item.id;
        }
        public void ItemHide()
        {
            TitleArea.gameObject.SetActive(false);
            DescriptionArea.gameObject.SetActive(false);
            ItemImage.gameObject.SetActive(false);
        }
        public void ItemShow()
        {
            TitleArea.gameObject.SetActive(true);
            DescriptionArea.gameObject.SetActive(true);
            ItemImage.gameObject.SetActive(true);
        }
    }
}