using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public GameObject UI;
    public GameObject DestroyWall; // If I wanted to remove an object
    public static bool pickRANGE; // If in object range
    private bool inRange;
    public bool isExpection;


    // TEMP
    public AudioSource source;
    public AudioClip clip1;

    public bool destroyable;

    // Start is called before the first frame update
    void Start()
    {
        pickRANGE = false; // prevents loops
        if (UI != null) // Prevents Error Logs
        {
            UI.SetActive(false); // Off
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetButtonDown("Interact") && !destroyable && !isExpection) // if it range of Trigger, Press E, and to prevent it from being destroyed.
        {
            if (source != null) // Play the temp audio (FOR LOCKER)
            {
                source.PlayOneShot(clip1);
            }


            UI.SetActive(false); // turn it off
            if (DestroyWall != null) // For if i want to remove an object
                GameObject.Destroy(DestroyWall);
            StartCoroutine(Wait1Second());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            inRange = true; // In range
            UI.SetActive(true); // Turn on UI
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            inRange = false; // No longer in range
            UI.SetActive(false); // Turn off the UI
        }

        if (isExpection) // The exception UI
        {
            UI.SetActive(false);
            StartCoroutine(Wait1Second());
        }
    }

    IEnumerator Wait1Second()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject); // Destroy itself
    }
}
