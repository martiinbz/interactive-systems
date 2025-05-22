using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCenter : MonoBehaviour
{
    public Transform center;
    public Transform character;

    void LateUpdate()
    {
        Vector3 direction = (center.position - transform.position).normalized;
        direction.y = 0; // Keep the y component zero to only rotate around the y-axis
        
        direction.Normalize();

        transform.position = character.position + direction * 4f;
        transform.rotation = Quaternion.LookRotation(-direction);
    }
}
