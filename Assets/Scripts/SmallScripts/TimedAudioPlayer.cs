using System.Collections;
using UnityEngine;

public class TimedAudioPlayer : MonoBehaviour
{
    public GameObject audioObject;
    public float timeDuration = 5f;

    void Start()
    {
        if (audioObject == null)
        {
            Debug.LogError("Audio GameObject not assigned!");
            return;
        }

        audioObject.SetActive(false);
        Debug.Log("Coroutine started");
        StartCoroutine(DelayedTime());
    }

    IEnumerator DelayedTime()
    {
        yield return new WaitForSeconds(timeDuration);

        audioObject.SetActive(true);
    }
}