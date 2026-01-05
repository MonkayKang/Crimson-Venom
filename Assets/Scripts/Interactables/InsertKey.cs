using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertKey : MonoBehaviour
{
    public bool redDOOR; // Is it a blue keypad
    public static bool rDoorON; // Is gadget blue (FOUND IN GADGET SCRIPT)
    private bool rdoonRADIUS; // Within Radius of red door
    public GameObject lights; // Lights

    public GameObject uiPOP; // the pop up

    private Animator _anim;

    public GameObject destroyOBJ; // If we want to remove a door

    // Start is called before the first frame update
    void Start()
    {
        uiPOP.SetActive(false);
        lights.SetActive(false); //  Turn them off
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
                lights.SetActive(true);
                Player.GadgetANIM = true; // Play the animation for player
                Player.gadgetON = false;
                uiPOP.SetActive(false);
                rDoorON = false;
                if (destroyOBJ != null) // If we want something to be gone
                {
                    Destroy(destroyOBJ);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("player")) return;

        if (redDOOR)
        {
            rdoonRADIUS = true;
        }

        if (rDoorON)
        {
            uiPOP.SetActive(true);
        }
        else if (!rDoorON)
        {
            uiPOP.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("player")) return;

        uiPOP.SetActive(false);

        if (redDOOR)
        {
            rdoonRADIUS = false;
        }
    }
}