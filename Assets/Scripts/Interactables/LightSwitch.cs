using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject Lights; // Put the Lights in here

    private bool inRange; // Is the player within interacting distance

    // Start is called before the first frame update
    void Start()
    {
        if (Lights != null) // Reuseability in code
            Lights.SetActive(false); // Turns it off at load
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetButtonDown("Interact")) // While the player is in range
        {
            Lights.SetActive(true); // Turn the lights on
            DoorOpen.isPowered = true; // The power is on
            Destroy(gameObject); // Destroy itself

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player")) // If it touches the player
        {
            inRange = true; // Then the player can interact with it
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player")) // If it leaves the player
        {
            inRange = false; // Then the player can't interact with it
        }
    }

    IEnumerator Wait1Sec()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);// Destroy itself
    }
}
