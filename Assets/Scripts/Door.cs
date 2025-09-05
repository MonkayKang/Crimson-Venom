using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator targetAnimator;
    public string boolName = "isOn"; // the name of the bool in the Animator

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Set the bool to false
            targetAnimator.SetBool(boolName, false);
        }
    }
}
