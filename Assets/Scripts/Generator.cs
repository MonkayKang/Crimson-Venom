using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] objects;
    private bool nearPlayer = false;

    // Update is called once per frame
    void Update()
    {
        if (nearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            UICounter.taskCounter++; // Add a new task
            for (int i = 0; i < objects.Length; i++) // For every object in that
            {
                if (objects[i] != null)
                {
                    Destroy(objects[i]);
                    objects[i] = null; // clears the reference
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("player"))
        {
            nearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            nearPlayer = false;
        }
    }
}
