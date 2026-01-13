using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backup : MonoBehaviour
{
    private bool nearPLAYER;

    // Pop up
    public Image readUI;
    public GameObject popupOBJ;
    public Sprite imageSLOT;


    // Inventory
    public Image InventoryImage;

    // Audio
    public AudioSource source;
    public AudioClip pickSFX;


    // Start is called before the first frame update
    void Start()
    {
        popupOBJ.SetActive(false); // Set the pop up UI true
        if (InventoryImage != null )
            InventoryImage.enabled = false;

        // Find those names
        source = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nearPLAYER && Input.GetButtonDown("Interact"))
        {
            if (source != null)
                source.PlayOneShot(pickSFX); // Play the SFX

            if (InventoryImage != null)
                InventoryImage.enabled = true;

            popupOBJ.SetActive(true); // Set the pop up UI true

            Inventory.newITEM = true;

            readUI.enabled = true;
            readUI.sprite = imageSLOT;
            Player.timeSTOP = true; // time has stopped

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
