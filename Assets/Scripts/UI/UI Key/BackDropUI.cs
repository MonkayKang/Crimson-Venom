using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDropUI : MonoBehaviour
{
    private bool isNear;
    public GameObject UI;
    // Update is called once per frame

    private void Start()
    {
        UI.SetActive(false);
    }
    void Update()
    {
        if (isNear)
        {
            if (Input.GetButtonDown("Menu"))
            {
                UI.SetActive(false);
                Player.STOP = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            isNear = true;
            UI.SetActive(true);
            Player.STOP = true;
        }
    }
}
