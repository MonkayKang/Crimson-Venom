using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTog : MonoBehaviour
{
    public GameObject trackedOBJ;
    private MeshRenderer m_MeshRenderer;
    public bool isE;
    private bool inside;

    private void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        inside = false;
        m_MeshRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && inside)
        {
            Destroy(gameObject); // Destroy itself
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player") && trackedOBJ != null)
        {
            inside = true;
            m_MeshRenderer.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            inside = false;
            m_MeshRenderer.enabled = false;
        }
    }
}
