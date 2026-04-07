using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour // SAME AS INTERACTABLES BUT WITH A LOCKED DOOR
{
    private bool inRadius;
    public bool dontDestroy; // For certain properties
    public GameObject LockUIpopup;
    public GameObject UnlockUIpopup;

    // Audio
    private AudioSource source;
    public AudioClip UnlockSFX;


    private void Start()
    {
        LockUIpopup.SetActive(false);
        UnlockUIpopup.SetActive(false);
        source = GameObject.Find("Player").GetComponent<AudioSource>(); // Audio is the Player
    }

    // Update is called once per frame
    void Update()
    {

        if (inRadius && NewBehaviourScript.hasKey == true) // This will appear when there is a key
        {
            UnlockUIpopup.SetActive(true); // The UI is ON
            if (Input.GetButtonDown("Interact"))
            {
                if (!dontDestroy)
                {
                    NewBehaviourScript.hasKey = false;
                    source.PlayOneShot(UnlockSFX);
                    StartCoroutine(DelayedDestroyed());
                }
                    
            }
        }
        else if (inRadius) // This will appear when there is no Key
        {
            LockUIpopup.SetActive(true); // The UI is Off
        }
        else
        {
            LockUIpopup.SetActive(false); // UI is OFF
            UnlockUIpopup.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            inRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            inRadius = false;
        }
    }

    IEnumerator DelayedDestroyed()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
