using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Toggle1 : MonoBehaviour
{
    public GameObject OBJ;
    // Start is called before the first frame update
    void Start()
    {
        OBJ.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            OBJ.SetActive(true);
        }
    }
}
