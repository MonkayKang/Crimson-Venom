using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public GameObject Enemy;

    private void Start()
    {
        Enemy.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player")) 
            Enemy.SetActive(true);
    }
}
