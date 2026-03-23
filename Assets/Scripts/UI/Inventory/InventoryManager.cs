using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivate;

    public ItemSlot[] itemSlots;     // normal items
    public ItemSlot[] keyItemSlots;  // key items

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            menuActivate = !menuActivate;

            InventoryMenu.SetActive(menuActivate);
            Time.timeScale = menuActivate ? 0 : 1;

            Cursor.lockState = menuActivate ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = menuActivate;
        }
    }

    public void AddItem(string itemName, Sprite itemSprite)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (!itemSlots[i].isFull)
            {
                itemSlots[i].AddItem(itemName, itemSprite);
                return;
            }
        }
    }

    public void AddKeyItem(string itemName, Sprite itemSprite)
    {
        for (int i = 0; i < keyItemSlots.Length; i++)
        {
            if (!keyItemSlots[i].isFull)
            {
                keyItemSlots[i].AddItem(itemName, itemSprite);
                return;
            }
        }
    }
}
