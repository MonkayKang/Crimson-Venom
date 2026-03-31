using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRemove : MonoBehaviour
{
    public string requiredItemName; // the item used

    private bool nearPlayer;
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    void Update()
    {
        if (nearPlayer && Input.GetKeyDown(KeyCode.E)) // When near player and pressed "e"
        {
            if (HasItem(requiredItemName))
            {
                inventoryManager.RemoveItem(requiredItemName);

            }
            else
            {
                Debug.Log("No item found");
            }
        }
    }

    bool HasItem(string itemName)
    {
        foreach (var slot in inventoryManager.itemSlots)
        {
            if (slot.isFull && slot.itemName == itemName)
                return true;
        }

        foreach (var slot in inventoryManager.keyItemSlots)
        {
            if (slot.isFull && slot.itemName == itemName)
                return true;
        }

        return false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
            nearPlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
            nearPlayer = false;
    }
}
