using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStay : MonoBehaviour
{
    public GameObject ObjectONE;
    public GameObject ObjectTWO;

    private bool reversed = false;
    private bool inRange = false;

    void Start()
    {
        ObjectONE.SetActive(true);
        ObjectTWO.SetActive(false);
    }

    void Update()
    {
        if (inRange && Input.GetButtonDown("Interact"))
        {
            reversed = !reversed;

            ObjectONE.SetActive(!reversed);
            ObjectTWO.SetActive(reversed);
            Player.STOP = reversed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
            inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            inRange = false;
            reversed = false;

            ObjectONE.SetActive(true);
            ObjectTWO.SetActive(false);
            Player.STOP = false;
        }
    }
}