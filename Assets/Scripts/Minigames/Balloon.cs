using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public AudioSource source; // The place where the audio comes from
    public AudioClip clip; // The Audio being played

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            source.PlayOneShot(clip);
            Destroy(gameObject); // Destroy itself
        }
    }
}
