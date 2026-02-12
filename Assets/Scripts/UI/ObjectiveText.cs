using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveText : MonoBehaviour
{
    public string Text;
    public NewBehaviourScript taskUI;   // Find the UI Task Script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            taskUI.SetObjective(Text);
            Destroy(gameObject); // Destroy itself
        }
    }
}
