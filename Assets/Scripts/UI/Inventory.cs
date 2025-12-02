using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image InventoryUI;
    public Sprite defaultUI;
    public Sprite NotifiedUI;

    public GameObject Inventory1;
    public GameObject Inventory2;

    public GameObject InventoryPanel; // The full inventory UI panel
    public GameObject InventoryPanel2; // The full inventory UI panel
    private bool isOpen = false; // Tracks if inventory is open

    public static bool newITEM; // If it has new item

    // Bool
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        newITEM = false;
        InventoryUI.sprite = defaultUI; // Go to default
        InventoryPanel.SetActive(false); // Make sure it starts hidden
        // InventoryPanel2.SetActive(false); // Make sure it starts hidden
    }

    // Update is called once per frame
    void Update()
    {
        if (!newITEM)
        {
            InventoryUI.sprite = defaultUI; // No new item
        }
        else if (newITEM)
        {
            InventoryUI.sprite = NotifiedUI; // new item
        }

        // Toggle inventory on ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If new item notification is on, clear it when opening
            if (newITEM)
                newITEM = false;

            ToggleInventory();
        }

        if (isActive) // if Inventory is Active
        {
            Cursor.lockState = CursorLockMode.None; // Unlock cursor
        }

        if (!isActive)
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor for gameplay
        }
    }

    void ToggleInventory()
    {
        isOpen = !isOpen;
        InventoryPanel.SetActive(isOpen);

        if (isOpen)
        {
            Time.timeScale = 0f; // Stop time but allow UI interaction
            Cursor.visible = true;
            isActive = true;
        }
        else
        {
            Time.timeScale = 1f; // Resume time
            Cursor.visible = false;
            isActive = false;
        }
    }

    public void ButtonPressing()
    {
        ToggleInventory();
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor for gameplay
    }

    public void SWITCH1() // Inventory
    {
        Inventory1.SetActive(true);
        Inventory2.SetActive(false);
    }

    public void SWITCH2() // Helpful Tips
    {
        Inventory1.SetActive(false);
        Inventory2.SetActive(true);
    }
}

