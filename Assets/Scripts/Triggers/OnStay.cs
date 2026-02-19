using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStay : MonoBehaviour
{
    public GameObject ObjectONE;
    public GameObject ObjectTWO;

    private bool isReversed = false; // A flip switch

    void Start()
    {
        ObjectONE.SetActive(true); // Have the first object in
        ObjectTWO.SetActive(false); // Have the second object out
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Trigger") && Input.GetButtonDown("Interact")) // While the player is inside and press "e"
        {
            isReversed = !isReversed; // flip state

            ObjectONE.SetActive(isReversed); // ON or OFF
            ObjectTWO.SetActive(!isReversed); // ON or OFF
        }
    }
}