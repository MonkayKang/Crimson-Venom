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

    // Start is called before the first frame update
    void Start()
    {
        popupOBJ.SetActive(false); // Set the pop up UI true
    }

    // Update is called once per frame
    void Update()
    {
        if (nearPLAYER && Input.GetKeyDown(KeyCode.E))
        {
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
