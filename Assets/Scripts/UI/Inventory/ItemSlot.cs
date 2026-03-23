using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item Data
    public string itemName;
    public Sprite itemSprite;
    public bool isFull;

    // Item Slot
    [SerializeField]
    private Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventorymanger;

    private void Start()
    {
        inventorymanger = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }


    public void AddItem(string itemName, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
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
    }

    public void OnRightClick()
    {

    }
}
