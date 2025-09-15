using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    // Start is called before the first frame update

    void Start()
    {
        text1.enabled = false;
        text2.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player") && text1 != null && text2 != null) // Make sure its reusable without having to use this part
        {
            text1.enabled = true;
            text2.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player") && text1 != null && text2 != null) // Make sure its reusable without having to use this part
        {
            text1.enabled = false;
            text2.enabled = false;
        }
    }
}
