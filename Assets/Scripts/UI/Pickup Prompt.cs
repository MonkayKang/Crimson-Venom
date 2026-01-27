using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPrompt : MonoBehaviour
{
    public GameObject promptUI;

    public void Start()
    {
        promptUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
            promptUI.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
            promptUI.SetActive(false);
    }


}
