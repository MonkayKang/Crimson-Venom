using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCondition : MonoBehaviour
{
    public GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UICounter.miniSCORE1 == 15)
        {
            for (int i = 0; i < objects.Length; i++) // For every object in that
            {
                if (objects[i] != null)
                {
                    Destroy(objects[i]);
                    objects[i] = null; // clears the reference
                }
            }
        }
    }
}
