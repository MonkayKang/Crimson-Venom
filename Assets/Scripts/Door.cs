using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator targetAnimator;
    public string boolName = "isOpen"; // the name of the bool in the Animator
    private bool open = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
}
