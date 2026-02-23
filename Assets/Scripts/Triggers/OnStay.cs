using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStay : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    public MeshRenderer playerMesh;
    public CapsuleCollider PlayerCapsuleCollider;

    public GameObject ObjectONE;
    public GameObject ObjectTWO;

    private bool reversed = false;
    private bool inRange = false;

    void Start()
    {
        // ObjectONE.SetActive(true);
        ObjectONE.SetActive(false);
    }

    void Update()
    {
        if (inRange && Input.GetButtonDown("Interact"))
        {
            reversed = !reversed;
            cam1.Priority = cam1.Priority == 10 ? 0 : 10;
            cam2.Priority = cam2.Priority == 10 ? 0 : 10;

            ObjectONE.SetActive(reversed);
            ObjectTWO.SetActive(!reversed);
            playerMesh.enabled = (!reversed); // Turn it off
            PlayerCapsuleCollider.enabled = (!reversed); // turn if off
            Player.STOP = reversed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
            inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        /*  if (other.CompareTag("Trigger"))
         {
             inRange = false;
             reversed = false;

             // ObjectONE.SetActive(true);
             ObjectONE.SetActive(false);
             playerMesh.enabled = true;
             PlayerCapsuleCollider.enabled = true;
             Player.STOP = false;
          */
    }
}