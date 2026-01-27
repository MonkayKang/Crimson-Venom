using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrackCanva : MonoBehaviour
{
    public GameObject TiedOBJ;

    void LateUpdate()
    {
        if (TiedOBJ == null)
            Destroy(gameObject); // if it no longer exist, destroy itself
        transform.forward = Camera.main.transform.forward;
    }
}
