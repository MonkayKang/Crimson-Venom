using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public GameObject DestroyWall; // If I wanted to remove an object
    private bool inRange;



    // TEMP
    public AudioSource source;
    public AudioClip clip1;


    // Start is called before the first frame update
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetButtonDown("Interact") && DestroyWall != null) // if it range of Trigger, Press E, and to prevent it from being destroyed.
        {
            if (source != null) // Play the temp audio (FOR LOCKER)
            {
                source.PlayOneShot(clip1);
            }

            GameObject.Destroy(DestroyWall);
            StartCoroutine(Wait1Second());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            inRange = true; // In range
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            inRange = false; // No longer in range
        }
    }

    IEnumerator Wait1Second()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject); // Destroy itself
    }
}
