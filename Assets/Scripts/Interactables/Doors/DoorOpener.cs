using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GameObject TiedObject;
    public GameObject Wall1; // The objects to set animations
    public GameObject Wall2;

    private Animator wall1Anim; // access the animation
    private Animator wall2Anim;

    void Start()
    {
        wall1Anim = Wall1.GetComponent<Animator>(); // Access the Anim
        wall2Anim = Wall2.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TiedObject == null)
        {
            wall1Anim.SetBool("Close", true);
            wall2Anim.SetBool("Close", true);
            Destroy(gameObject);
        }
    }
}
