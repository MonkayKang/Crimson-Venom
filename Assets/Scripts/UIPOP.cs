using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPOP : MonoBehaviour
{
    public GameObject popUP;

    private void Start()
    {
        popUP.SetActive(false); // disable it
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player")) // If it hits the player
        {
            popUP.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player")) // When they leave the zone
        {
            popUP.SetActive(false);
        }
    }
}
