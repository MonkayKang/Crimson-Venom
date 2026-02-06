using 
System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KeyUIPOPUP : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Activate()
    {
        StartCoroutine(PlayUIRoutine());
    }

    private IEnumerator PlayUIRoutine()
    {
        // Reset animation to idle
        anim.Play("Idle", 0, 0f);

        // Wait a tiny bit so the animator actually resets
        yield return new WaitForSeconds(0.25f);

        // Trigger the animation
        anim.SetTrigger("PlayUI");
    }
}

