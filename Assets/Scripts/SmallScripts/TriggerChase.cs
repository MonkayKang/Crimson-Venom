using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TriggerChase : MonoBehaviour
{
    public GameObject Enemy; // Chaser
    public Transform savedPos; // Teleportation


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            NavMeshAgent agent = Enemy.GetComponent<NavMeshAgent>();
            agent.Warp(savedPos.position);
            Enemy.transform.position = savedPos.position; // Teleport the Antagonist to the saved position
            Destroy(gameObject);
        }
    }
}
