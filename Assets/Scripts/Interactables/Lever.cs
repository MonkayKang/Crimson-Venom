using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject Lobjects; //  Objects to be loaded
    public GameObject Uobjects1; // Objects to be unloaded
    public GameObject Uobjects2; // Objects to be unloaded
    public GameObject DestroyOBJ; // destroy this object
    public GameObject UI; //Unload UI
    public static bool playerLEVER; // If the player has lever

    private bool inrange; // Player in range
    public bool isPickup; // Resuing the same code

    public GameObject[] UnloadPLURAL; // for multiple


    // Audio
    public AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        Lobjects.SetActive(false); // Turn this object off
        Uobjects1.SetActive(true); Uobjects2.SetActive(true); // turn these on
    }

    // Update is called once per frame
    void Update()
    {
        if (inrange && playerLEVER) // if in range and player has a lever
        {
            if (Input.GetButtonDown("Interact"))
            {
                source.PlayOneShot(clip);
                if (UI != null)
                {
                    UI.SetActive(false);
                }
                if (DestroyOBJ != null)
                {
                    Destroy(DestroyOBJ);
                }

                if (Uobjects1 != null && Uobjects2 != null)
                {
                    Uobjects1.SetActive(false);
                    Uobjects2.SetActive(false);
                }

                if (Lobjects != null)
                {
                    Lobjects.SetActive(true);
                }

                if (UnloadPLURAL != null)
                {
                    foreach (GameObject obj in UnloadPLURAL)
                    {
                        obj.SetActive(false);
                    }
                }

                playerLEVER = false;

                //  Reset lever in player's hand + UI
                Player playerScript = FindObjectOfType<Player>();
                if (playerScript != null)
                {
                    playerScript.LeverItem.SetActive(false);
                    playerScript.thirdSLOT.sprite = playerScript.blankUI;
                    RectTransform slotTransform3 = playerScript.thirdSLOT.GetComponent<RectTransform>();
                    slotTransform3.sizeDelta = new Vector2(250f, 250f);
                }
            }
        }

        if (inrange && isPickup && Input.GetButtonDown("Interact")) // If the player is in range and the lever is a pick up object
        {
                Lobjects.SetActive(true);
                Uobjects1.SetActive(false);
                Uobjects2.SetActive(false);
                Lever.playerLEVER = true;
            if (DestroyOBJ != null)
            {
                Destroy(DestroyOBJ); // get rid of it
            }
            StartCoroutine(Wait1Second());

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            inrange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            inrange = false;
        }
    }

    IEnumerator Wait1Second()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject); // Destroy itself
    }
}
