using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public float expansionSpeed = 2f;
    public float maxScale = 5f;

    void Update()
    {
        expansionSpeed += 0.1f * Time.time;
        float step = expansionSpeed * Time.deltaTime;
        transform.localScale += new Vector3(step, step, 0);
        maxScale += 0.1f * Time.time;
        if (transform.localScale.x >= maxScale)
        {
            Destroy(gameObject); // Remove wave after expanding
        }
    }
}
