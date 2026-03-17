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

    private InventoryManager inventoryManager;

    public bool dontDestroy; // Certain Conditions
    
    // Audio
    private AudioSource source;
    public AudioClip pickSFX;

    private bool nearPlayer;
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

        // Find those names
        source = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nearPlayer && Input.GetButtonDown("Interact"))
        {
            inventoryManager.AddItem(itemName, sprite);
            source.PlayOneShot(pickSFX); // Play the SFX

            if (!dontDestroy)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            nearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            nearPlayer = false;
        }
    }
}
