using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterInteract : MonoBehaviour
{
    private bool inRadius;
    public GameObject UIpopup;

    // Audio
    private AudioSource source;
    public AudioClip pickSFX;

    public bool isKey;


    private void Start()
    {
        UIpopup.SetActive(false);
        source = GameObject.Find("Player").GetComponent<AudioSource>(); // Audio is the Player
    }

    // Update is called once per frame
    void Update()
    {
        if (inRadius)
        {
            UIpopup.SetActive(true); // The UI is Off
            if (Input.GetButtonDown("Interact"))
            {
                if (isKey)
                {
                    NewBehaviourScript.hasKey = true;
                }

                source.PlayOneShot(pickSFX);
                Destroy(gameObject);
            }
        }
        else
        {
            UIpopup.SetActive(false); // UI is On
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
}
