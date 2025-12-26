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
        page1.UpdateItem(itemList.GetItem(1));
        page2.UpdateItem(itemList.GetItem(2));
        leftPageNumber.text = (pageNumber * 2 - 1).ToString();
        rightPageNumber.text = (pageNumber * 2).ToString();
    }
    public void PageUp()
    {
        if (itemList.GetLength() > pageNumber * 2)
        {
            pageNumber += 1;
            page1.UpdateItem(itemList.GetItem(pageNumber * 2 - 1));
            leftArrow.interactable = true;
            if (itemList.GetLength() > pageNumber * 2)
            {
                page2.UpdateItem(itemList.GetItem(pageNumber * 2));
            }
            else
            {
                page2.ItemHide();
                rightArrow.interactable = false;
            }
            leftPageNumber.text = (pageNumber * 2 - 1).ToString();
            rightPageNumber.text = (pageNumber * 2).ToString();
        }
    }
    public void PageDown()
    {
        pageNumber--;
        page1.UpdateItem(itemList.GetItem(pageNumber * 2 - 1));
        page2.UpdateItem(itemList.GetItem(pageNumber * 2));
        leftPageNumber.text = (pageNumber * 2 - 1).ToString();
        rightPageNumber.text = (pageNumber * 2).ToString();
        page2.ItemShow();
        rightArrow.interactable = true;
        if (pageNumber == 1) leftArrow.interactable = false;
    }


    [Serializable]
    public class Page {
        public TextMeshProUGUI TitleArea;
        public TextMeshProUGUI DescriptionArea;
        public Image ItemImage;
        public int ID;

        public void UpdateItem(Item item)
        {
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