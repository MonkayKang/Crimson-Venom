using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public static bool isPowered;
    public bool Opener;

    public GameObject Wall1; // The objects to set animations
    public GameObject Wall2;

    private Animator wall1Anim; // access the animation
    private Animator wall2Anim;

    void Start()
    {
        isPowered = false;
        wall1Anim = Wall1.GetComponent<Animator>(); // Access the Anim
        wall2Anim = Wall2.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If openener
        if (other.CompareTag("player") && isPowered && Opener) 
        {
            wall1Anim.SetBool("Close", true);
            wall2Anim.SetBool("Close", true);
        }

        // If closer
        if (other.CompareTag("player") && !Opener)
        {
            wall1Anim.SetBool("Close", false);
            wall2Anim.SetBool("Close", false);
        }
    }
}