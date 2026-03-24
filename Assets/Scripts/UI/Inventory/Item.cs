using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Item : MonoBehaviour
{
    [SerializeField] 
    private string itemName;

    [SerializeField] 
    private Sprite sprite;

    [SerializeField]
    private string itemDescription;

    public bool isKeyItem;
    public bool dontDestroy;

    private InventoryManager inventoryManager;

    private AudioSource source;
    public AudioClip pickSFX;

    private bool nearPlayer;
    private bool onlyOnce;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        source = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (nearPlayer && Input.GetButtonDown("Interact") && !onlyOnce)
        {
            if (isKeyItem )
                inventoryManager.AddKeyItem(itemName, sprite, itemDescription);      
            else
                inventoryManager.AddItem(itemName, sprite, itemDescription);

            source.PlayOneShot(pickSFX);
            onlyOnce = true; // prevents duplications

            if (!dontDestroy)
                Destroy(gameObject);
        }
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
