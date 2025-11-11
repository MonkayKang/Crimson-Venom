using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEvent : MonoBehaviour
{
    public GameObject obj;

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            obj.SetActive(true);
        } 
    }
}
