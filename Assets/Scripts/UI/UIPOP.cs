using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIPOP : MonoBehaviour
{
    public GameObject popUP;
    public MonoBehaviour script;

    public bool Used = false;

    bool nearPlayer; // is the object near the player

    private void Start()
    {
        popUP.SetActive(false); // disable it
    }

    // Update is called once per frame
    void Update()
    {
        if (nearPlayer && !Used && Input.GetKeyDown(KeyCode.E))
        {
            popUP.SetActive(false);
            Used = true; // Already been used
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player") && !Used) // If it hits the player
        {
            popUP.SetActive(true);
            nearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player") && !Used) // When they leave the zone
        {
            popUP.SetActive(false);
            nearPlayer = false;
        }
    }
}
