using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkEmmiter : MonoBehaviour
{
    public GameObject sparkPrefab;
    public int sparkPerSecond = 10;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        float interval = 1f / sparkPerSecond;

        while (timer >= interval)
        {
            timer -= interval;
            SpawnSpark();
        }
    }

    void SpawnSpark()
    {
        Instantiate(
            sparkPrefab,
            transform.position,
            Quaternion.identity
        );
    }
}
