using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyItem : MonoBehaviour
{
    public Image UI; // The UI it will effect
    public Sprite ImageReplace;

    public bool isPickUP;
    public bool isTrigger;
    private bool isNear;

    public KeyUIPOPUP popup; // assign this in the Inspector

    private void Start()
    {

    }
    private void Update()
    {
        if (isPickUP && isNear && Input.GetButtonDown("Interact"))
        {
                popup.Activate(); // Activate the code
                UI.sprite = ImageReplace;

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            isNear = true;

            if (isTrigger)
            {
                popup.Activate(); // Activate the code
                UI.sprite = ImageReplace;
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"));
            isNear = false;
    }

}
