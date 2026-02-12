using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    public GameObject throwPrefab; // The throw object
    public Transform throwPoint;      // Where it spawns
    public float throwForce = 10f; // How strong
    public float lifetime = 3f; // How long it lasts

    void Update()
    {
        if (MinigameRange.InRange && Input.GetMouseButtonDown(0))
        {
            ThrowObject(); // If they trigger the range, 
        }
    }

    void ThrowObject()
    {
        GameObject obj = Instantiate(throwPrefab, throwPoint.position, throwPoint.rotation);

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);

        Destroy(obj, lifetime);
    }
}
