using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivate;
    public ItemSlot[] itemSlots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            menuActivate = !menuActivate;

            InventoryMenu.SetActive(menuActivate);
            Time.timeScale = menuActivate ? 0 : 1;

            if (menuActivate)
            {
                // Inventory OPEN 
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                // Inventory CLOSED 
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void AddItem(string itemName, Sprite itemSprite)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].isFull == false)
            {
                itemSlots[i].AddItem(itemName, itemSprite);
                return;
            }
        }
    }
}
