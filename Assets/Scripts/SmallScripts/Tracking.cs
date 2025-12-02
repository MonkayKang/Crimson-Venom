using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    public Transform player;  // Assign the player's transform

    void Update()
    {
        if (player == null) return;

        // Rotate this object to face the player
        transform.LookAt(player);
    }
}
