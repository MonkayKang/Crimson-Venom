using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Generator : MonoBehaviour
{
    public GameObject[] objects; // Objects that will be removed
    public GameObject Antagonist1; // Objects that will be spawned
    public GameObject Antagonist2; //

    private bool nearPlayer = false;

    public GameObject Enemy; // Whos being telported
    public GameObject Enemy2; // Whos being telported
    public Transform savedPos; // Teleportation

    // Update is called once per frame
    void Update()
    {
        if (nearPlayer && Input.GetKeyDown(KeyCode.E))
        {

            NavMeshAgent agent = Enemy.GetComponent<NavMeshAgent>(); // Spawn the Enemies into the world
            agent.Warp(savedPos.position);
            Enemy.transform.position = savedPos.position;

            NavMeshAgent agent2 = Enemy2.GetComponent<NavMeshAgent>(); // Spawn the Enemies into the world
            agent2.Warp(savedPos.position);
            Enemy.transform.position = savedPos.position;


            UICounter.taskCounter++; // Add a new task
            for (int i = 0; i < objects.Length; i++) // For every object in that
            {
                if (objects[i] != null)
                {
                    Destroy(objects[i]);
                    objects[i] = null; // clears the reference
                }
            }

            Antagonist1.SetActive(true);
            Antagonist2.SetActive(true);
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
