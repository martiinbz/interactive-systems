using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHitDetector : MonoBehaviour
{
    public enum WaveType { TypeA, TypeB }
    public WaveType waveType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedPlayer") || other.CompareTag("BluePlayer"))
        {
            float yPos = other.transform.position.y;

            bool isHit = false;

            string objName = other.gameObject.name;

            // Example logic: different wave types only hit within certain Y ranges
            if (waveType == WaveType.TypeA && yPos < 1.5)
                isHit = true;
            else if (waveType == WaveType.TypeB && yPos >= 0.6)
                isHit = true;

            if (isHit)
            {

                if (objName.Contains("1"))
                    GameManager.Instance.RegisterHit("Red");
                else if (objName.Contains("2"))
                    GameManager.Instance.RegisterHit("Blue");
            }
        }
    }
}
