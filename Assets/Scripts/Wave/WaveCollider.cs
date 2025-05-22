using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCollider : MonoBehaviour
{
    public float expansionSpeed = 5f;
    public float maxScale = 10f;

    public GameManager gameManager;

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
        if(other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.name == "Player1")
            {
                if(GameManager.Instance != null)
                {
                    GameManager.Instance.RegisterHit("Red");
                }
                
            }
            else
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.RegisterHit("Blue");
                }
            }
        }
        //for the moment
    }
}
