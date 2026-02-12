using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameRange : MonoBehaviour
{
    public GameObject ThrowingObject; // The object that the player is tied to
    public static bool InRange; // If the player is in range

    private void Start()
    {
        InRange = false;
        ThrowingObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger")) // Player is within range
        {
            InRange = true;
            ThrowingObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger")) // Player is out of range
        {
            InRange = false;
            ThrowingObject.SetActive(false);
        }
    }
}
