using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator targetAnimator;
    public string boolName = "isOpen"; // the name of the bool in the Animator
    private bool open = true;
    private bool playerNear;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerNear)
        {
            if (open)
            {
                // Close door
                targetAnimator.SetBool(boolName, false);
                open = false;
            }
            else
            {
                // Open door
                targetAnimator.SetBool(boolName, true);
                open = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            playerNear = false;
        }
    }
}
