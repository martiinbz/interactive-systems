using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBullets : MonoBehaviour
{
    public GameObject prefabBullet;
    public float spawnRadius = 45f;
    public float spawnInterval = 5f;
    public int maxObjects = 3;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(TrySpawn), 0f, spawnInterval);
    }

    void TrySpawn()
    {
        if(!GameManager.Instance.roundActive)
        {
            return; // Do not spawn during active rounds
        }
        spawnedObjects.RemoveAll(obj => obj == null);

        if(spawnedObjects.Count >= maxObjects)
        {
            return;
        }

        Vector3 spawnPosition = GetRandomPointInCircleXZ(spawnRadius);
        GameObject newObj = Instantiate(prefabBullet, spawnPosition, Quaternion.identity);
        spawnedObjects.Add(newObj);
    }
    
    Vector3 GetRandomPointInCircleXZ(float radius)
    {
        Vector2 randomCircle = Random.insideUnitCircle * radius;
        return new Vector3(randomCircle.x, 0f, randomCircle.y);
    }
}
