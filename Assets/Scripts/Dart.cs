using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    public float lifeTime = 3f; // how long before the dart decays;

    void Start()
    {
        StartCoroutine(Decay());
    }


    IEnumerator Decay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
