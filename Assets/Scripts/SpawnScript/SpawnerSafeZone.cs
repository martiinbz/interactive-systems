using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSafeZone : MonoBehaviour
{
    public GameObject prefabZone;
    public Transform[] spawnPoints; //4 points of spawn
    public float spawnInterval = 30f;
    public int maxObjects = 1;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(TrySpawn), 0f, spawnInterval);
    }

    void TrySpawn()
    {
        if (!GameManager.Instance.roundActive)
        {
            return; // Do not spawn during active rounds
        }
        spawnedObjects.RemoveAll(obj => obj == null);

        if (spawnedObjects.Count >= maxObjects)
        {
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject newObj = Instantiate(prefabZone, spawnPoint.position, Quaternion.identity);
        spawnedObjects.Add(newObj);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.name == "Player1")
            {
                GameManager.Instance.play1_safe = true;
            }
            else
            {
                GameManager.Instance.play2_safe = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.name == "Player1")
            {
                GameManager.Instance.play1_safe = false;
            }
            else
            {
                GameManager.Instance.play2_safe = false;
            }
        }
    }
}
