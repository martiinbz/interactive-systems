using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCollider : MonoBehaviour
{
    public float expansionSpeed = 5f;
    public float maxScale = 10f;
    public GameManager gamescript;
    private Vector3 startScale;


    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float scaleStep = expansionSpeed * Time.deltaTime;
        transform.localScale += Vector3.one * scaleStep;

        if (transform.localScale.x >= maxScale)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Wave hit: " + other.name);

        //for the moment
        if (other.name.ToLower().Contains("Red"))
        {
            gamescript.RegisterHit("Red");
        }
        else if (other.name.ToLower().Contains("Blue"))
        {
            gamescript.RegisterHit("Blue");
        }


    }
}
