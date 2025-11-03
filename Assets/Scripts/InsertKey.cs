using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertKey : MonoBehaviour
{
    public bool redDOOR; // Is it a red keypad
    public static bool rDoorON; // Is gadget red?
    private bool rdoonRADIUS; // Within Radius of red door

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        rDoorON = false; // prevents loops
    }

    // Update is called once per frame
    void Update()
    {
        if (rDoorON && rdoonRADIUS)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _anim.SetTrigger("InsertKey"); // Play the animation
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player") && redDOOR) // If it connects with the gadget and is a red door
        {
            rdoonRADIUS = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player") && redDOOR) // Once the player leaves
        {
            rdoonRADIUS = false;
        }
    }
}
