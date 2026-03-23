using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item Data //
    public string itemName;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;

    // Item Slot //
    [SerializeField]
    private Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;

    // Item Description //
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionText;
    public TMP_Text itemNameText;

    private InventoryManager inventorymanger;

    private void Start()
    {
        inventorymanger = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }


    public void AddItem(string itemName, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        itemImage.sprite = itemSprite;
        itemImage.enabled = true; // IMPORTANT
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button== PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }

        if(eventData.button== PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventorymanger.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;

        itemNameText.text = itemName; // set the item text to the name of it
        itemDescriptionText.text = itemDescription; // set the item decription
        itemDescriptionImage.sprite = itemSprite; // set the description sprite
    }

    public void OnRightClick()
    {

    }
}
