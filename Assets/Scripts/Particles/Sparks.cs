using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour
{
    float life = 0.3f;
    SpriteRenderer sr;
    Rigidbody rb;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        life -= Time.deltaTime;

        Color c = sr.color;
        c.a = life / 0.3f;
        sr.color = c;

        if (life <= 0f)
            Destroy(gameObject);
    }

    void LateUpdate()
    {
        if (rb.velocity.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }
}
