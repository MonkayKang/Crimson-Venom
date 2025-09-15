using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ballon : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) // if a dart hits it
        {
            source.PlayOneShot(clip); // Play the "POP" noise
            UICounter.miniSCORE1 += 1; // Add one to the score
            Destroy(collision.gameObject); // Destroy the dart
            Destroy(gameObject); // Destroy itself
        }
    }
}
