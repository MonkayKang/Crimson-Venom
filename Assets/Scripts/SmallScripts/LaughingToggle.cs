using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughingToggle : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    private bool hasPlayed;
    // Start is called before the first frame update
    void Start()
    {
        hasPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player") && Lever.playerLEVER && !hasPlayed)
        {
            source.PlayOneShot(clip);
            hasPlayed = true;
        }
    }
}
