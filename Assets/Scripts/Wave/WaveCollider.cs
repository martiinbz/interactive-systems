using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCollider : MonoBehaviour
{
    public float expansionSpeed = 2f;
    public float maxScale = 1f;

    private Vector3 startScale;

    public bool wave_color = false;

    public bool original_player = false; //true = red, false = blue

    private float lastHitTime = -Mathf.Infinity;

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

    public void setoriginal_player(bool original)
    {
        original_player = original;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Original_player: " + original_player);
        if (Time.time - lastHitTime < 0.5f)
        {
            return; // Ignore if the last hit was within the time window
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.name == "Player1")
            {
                if (!original_player)
                {
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.RegisterHit("Red", wave_color);
                        Debug.Log("Wave_color: " + wave_color);
                        lastHitTime = Time.time; // Update the last hit time
                    }
                }
                
            }
            else
            {
                if (original_player)
                {
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.RegisterHit("Blue", wave_color);
                        Debug.Log("Wave_color: " + wave_color);
                        lastHitTime = Time.time; // Update the last hit time
                    }
                }
                    
            }
        }
    }
}
