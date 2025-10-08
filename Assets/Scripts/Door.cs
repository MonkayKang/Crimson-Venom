using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Animator targetAnimator;
    public string boolName = "isOpen"; // the name of the bool in the Animator
    private bool open = true;
    private bool playerNear;

    //Detection UI
    public Image Detection;
    public Sprite closedEYE;
    public Sprite openEYE;

    void Update()
    {
        if (UICounter.inChase && playerNear) // While the player is still in chase but hide in a locker
        {
            StartCoroutine(Delay()); // Give a few sec to see if jumpscares appear
        }

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

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(5f);
        if (UICounter.inChase && playerNear) // While the player is still in chase but hide in a locker
        {
            targetAnimator.SetBool("Grab", true);
        }
        
    }
}

