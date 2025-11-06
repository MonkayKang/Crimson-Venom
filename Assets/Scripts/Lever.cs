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
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (UI != null)
                {
                    UI.SetActive(false);
                }
                if (DestroyOBJ != null)
                {
                    Destroy(DestroyOBJ); // get rid of it
                }

                if (Uobjects1 != null && Uobjects1 != null)
                {
                    Uobjects1.SetActive(false); Uobjects2.SetActive(false); // turn these off
                }
                
                if (Lobjects != null)
                {
                    Lobjects.SetActive(true); // Turn this object on
                }
                

                
            }
        }

        if (inrange && isPickup) // If the player is in range and the lever is a pick up object
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
