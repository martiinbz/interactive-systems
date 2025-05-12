using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{

    public GameObject wavePrefabcroach;    // Reference to the wave prefab
    public GameObject wavePrefabjump;    // Reference to the wave prefab
    public GameObject sensorObject;  // Reference to the sensor object
    public float waveHeightMultiplier = 1f; // Multiplier for wave height based on Z position

    public float lowerBound = 0; // Lower bound for the Z position, from normal to crouch
    public float upperBound = 1; // Upper bound for the Z position, from normal to jump

    //The idea is to make a pseudostate machine
    public float timeWindow = 0.5f; // Max time allowed between states

    private bool wasAbove = false;
    private bool wasBelow = false;
    private float lastAboveTime = -10f;
    private float lastBelowTime = -10f;

    public AudioClip crouchSound;
    public AudioClip jumpSound;
    public AudioSource audioSource;

    void Update()
    {
        if (sensorObject == null) return;

        float z = sensorObject.transform.position.y;
        float currentTime = Time.time;

        Vector3 wavePosition = new Vector3(sensorObject.transform.position.x, 0.5f, sensorObject.transform.position.z);
        Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);

        // ——— Pruebas en teclado ———
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Onda de salto con tecla “1”
            Instantiate(wavePrefabjump, wavePosition, rotation);
            PlaySound(jumpSound, wavePosition);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Onda de agacharse con tecla “2”
            Instantiate(wavePrefabcroach, wavePosition, rotation);
            PlaySound(crouchSound, wavePosition);
        }


        // Jump detection
        if (z > upperBound)
        {
            if (!wasAbove)
            {
                wasAbove = true;
                lastAboveTime = currentTime;

                if (currentTime - lastBelowTime <= timeWindow)
                {
                    // Jump after crouch ? spawn jump wave
                    Instantiate(wavePrefabjump, wavePosition, rotation);
                    PlaySound(jumpSound, wavePosition); // Play jump sound
                }
            }
        }
        else
        {
            wasAbove = false;
        }

        // Crouch detection
        if (z < lowerBound)
        {
            if (!wasBelow)
            {
                wasBelow = true;
                lastBelowTime = currentTime;

                if (currentTime - lastAboveTime <= timeWindow)
                {
                    // Crouch after jump ? spawn crouch wave
                    Instantiate(wavePrefabcroach, wavePosition, rotation);
                    PlaySound(crouchSound, wavePosition); // Play crouch sound
                }
            }
        }
        else
        {
            wasBelow = false;
        }


        void PlaySound(AudioClip clip, Vector3 position)
        {
            if (clip == null)
            {
                return;
            }
            if (audioSource != null)
            {
                audioSource.PlayOneShot(clip);
            }

            else
            {
                AudioSource.PlayClipAtPoint(clip, position);
            }
        }
    }
}
