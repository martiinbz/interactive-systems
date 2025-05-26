using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public float expansionSpeed = 2f;
    public float maxScale = 5f;

    void Update()
    {
        float step = expansionSpeed * Time.deltaTime;
        transform.localScale += new Vector3(step, step, 0);

        if (transform.localScale.x >= maxScale)
        {
            Destroy(gameObject); // Remove wave after expanding
        }
    }
}
