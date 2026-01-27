using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour
{
    Vector2 velocity;
    float life = 0.3f;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        transform.rotation = Quaternion.Euler(
            0f,
            0f,
            Random.Range(0f, 360f)
        );

        velocity = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(4f, 5f)
        );
    }

    void Update()
    {

        velocity.y -= 9.8f * Time.deltaTime;
        transform.position += (Vector3)(velocity * Time.deltaTime);

        life -= Time.deltaTime;

        Color c = sr.color;
        c.a = life / 0.3f;
        sr.color = c;

        if (life <= 0f)
            Destroy(gameObject);
    }
}
