using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LorePickup : MonoBehaviour
{
    private bool nearPLAYER;

    public Image inventorySPACE;
    public Sprite imageSLOT;

    public void Start()
    {
        inventorySPACE.enabled = false;
    }

    private void Update()
    {
        if (nearPLAYER && Input.GetKeyDown(KeyCode.E))
        {
            Inventory.newITEM = true;
            inventorySPACE.enabled = true;
            inventorySPACE.sprite = imageSLOT;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            nearPLAYER = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            nearPLAYER = false;
        }
    }
}
