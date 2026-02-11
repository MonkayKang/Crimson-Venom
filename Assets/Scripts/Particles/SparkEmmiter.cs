using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkEmmiter : MonoBehaviour
{
    public GameObject sparkPrefab;

    public int minSparks = 5;
    public int maxSparks = 20;

    public float minBurstDelay = 0.2f;
    public float maxBurstDelay = 1.0f;

    public float minForce = 2f;
    public float maxForce = 6f;

    // Audio
    public AudioSource sparksSFX;
    public AudioClip SparksClip;

    public float pitchMin = 0.9f;
    public float pitchMax = 1.1f;
    void Start()
    {
        StartCoroutine(BurstRoutine());
    }

    IEnumerator BurstRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minBurstDelay, maxBurstDelay));

            int sparkCount = Random.Range(minSparks, maxSparks + 1);

            for (int i = 0; i < sparkCount; i++)
            {
                SpawnSpark();
            }
        }
    }

    void SpawnSpark()
    {
        sparksSFX.pitch = Random.Range(pitchMin, pitchMax); // Slight pitch variation for realism
        sparksSFX.PlayOneShot(SparksClip); // Play sparks Sfx
        GameObject spark = Instantiate(
            sparkPrefab,
            transform.position,
            Quaternion.identity
        );

        Rigidbody rb = spark.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomDir = new Vector3(
                Random.Range(-1f, 1f),   // X spread
                Random.Range(0.5f, 1f),  // Y bias UP
                Random.Range(-1f, 1f)    // Z spread
            ).normalized;
            float force = Random.Range(minForce, maxForce);
            rb.AddForce(randomDir * force, ForceMode.Impulse);
        }
    }
}
