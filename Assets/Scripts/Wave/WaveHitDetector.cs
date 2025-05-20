using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHitDetector : MonoBehaviour
{
    public enum WaveType { TypeA, TypeB }
    public WaveType waveType;
    

    [Tooltip("Tiempo mínimo entre impactos (segundos)")]
    public float hitCooldown = 0.5f;          // ½ segundo de cooldown por defecto
    private float lastHitTime = -Mathf.Infinity;

    private void OnTriggerEnter(Collider other)
    {
        
        if (!other.CompareTag("RedPlayer") && !other.CompareTag("BluePlayer"))
            return;

        // Si aún no ha pasado el cooldown, salimos
        if (Time.time < lastHitTime + hitCooldown)
            return;

        float yPos = other.transform.position.y;
        bool isHit = false;
        string objName = other.gameObject.name;

        // Lógica de impacto según tipo de onda y altura
        if (waveType == WaveType.TypeA && yPos < 1.5f)
            isHit = true;
        else if (waveType == WaveType.TypeB && yPos >= 0.6f)
            isHit = true;

        if (isHit)
        {
            // Registramos el hit en el GameManager
            if (objName.Contains("Red"))
                GameManager.Instance.RegisterHit("Blue");
            else if (objName.Contains("Blue"))
                GameManager.Instance.RegisterHit("Red");

            // Actualizamos el momento del último hit
            lastHitTime = Time.time;
        }
    }
}
