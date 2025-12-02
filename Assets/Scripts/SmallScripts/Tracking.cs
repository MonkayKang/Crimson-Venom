using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    private Transform player;  // Assign the player's transform

    void Start()
    {
        GameObject obj = GameObject.Find("Player"); // find the player
        player = obj.transform; // the "player" is that object

    }

    void Update()
    {
        if (player == null) return;

        // Rotate this object to face the player
        transform.LookAt(player);
    }
}
