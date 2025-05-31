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
    public float timeWindow = 2.0f; // Max time allowed between states

    private bool wasAbove = false;
    private bool wasBelow = false;
    private float lastAboveTime = -10f;
    private float lastBelowTime = -10f;

    private int bullets = 2;

    public float waveCooldown = 1.5f;

    private float lastWaveTimePlayer1 = -10f;
    private float lastWaveTimePlayer2 = -10f;


    public AudioClip crouchSound;
    public AudioClip jumpSound;
    public AudioSource audioSource;

    void Update()
    {
        bullets = getbulletsfromManager();
        //Debug.Log("Bullets: " + bullets);
        if (sensorObject == null) return;

        float z = sensorObject.transform.position.y;
        float currentTime = Time.time;

        Vector3 wavePosition = new Vector3(sensorObject.transform.position.x, 0.5f, sensorObject.transform.position.z);
        Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);

        // ��� Pruebas en teclado ���
        if (Input.GetKeyDown(KeyCode.Alpha1) && bullets > 0 && CanGenerateWave())
        {
            // Onda de salto con tecla �1�
            GameObject wave = Instantiate(wavePrefabjump, wavePosition, rotation);
            WaveCollider wavescript = wave.GetComponent<WaveCollider>();
            Debug.Log("Wavescript: " + wavescript);
            if (sensorObject.name == "Player1")
            {
                wavescript.setoriginal_player(true);
            }
            else
            {
                wavescript.setoriginal_player(false);
            }
            updatebulletstoManager();
            PlaySound(jumpSound, wavePosition);
            UpdateLastWaveTime();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && bullets > 0 && CanGenerateWave())
        {
            // Onda de agacharse con tecla �2�
            GameObject wave = Instantiate(wavePrefabcroach, wavePosition, rotation);
            WaveCollider wavescript = wave.GetComponent<WaveCollider>();
            Debug.Log("Wavescript: " + wavescript);
            if (sensorObject.name == "Player1")
            {
                wavescript.setoriginal_player(true);
            }
            else
            {
                wavescript.setoriginal_player(false);
            }
            updatebulletstoManager();
            PlaySound(crouchSound, wavePosition);
            UpdateLastWaveTime();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            lowerBound -= 0.1f;
            Debug.Log("Lower Bound: " + lowerBound);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            lowerBound += 0.1f;
            Debug.Log("Lower Bound: " + lowerBound);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            upperBound -= 0.1f;
            Debug.Log("Upper Bound: " + upperBound);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            upperBound += 0.1f;
            Debug.Log("Upper Bound: " + upperBound);
        }


        // Jump detection
        if (z > upperBound)
        {
            if (sensorObject.name == "Player1")
                GameManager.Instance.stateplayer1 = 2; // Set player state to jump
            else
                GameManager.Instance.stateplayer2 = 2; // Set player state to jump
            if (!wasAbove)
            {
                wasAbove = true;
                lastAboveTime = currentTime;

                if (currentTime - lastBelowTime <= timeWindow && bullets > 0 && CanGenerateWave())
                {
                    GameObject wave = Instantiate(wavePrefabjump, wavePosition, rotation);
                    WaveCollider wavescript = wave.GetComponent<WaveCollider>();
                    Debug.Log("Wavescript: " + wavescript);
                    if (sensorObject.name == "Player1")
                    {
                        wavescript.setoriginal_player(true);
                    }
                    else
                    {
                        wavescript.setoriginal_player(false);
                    }
                    updatebulletstoManager();
                    PlaySound(jumpSound, wavePosition); // Play jump sound
                    UpdateLastWaveTime();
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
            if (sensorObject.name == "Player1")
                GameManager.Instance.stateplayer1 = 1; // Set player state to jump
            else
                GameManager.Instance.stateplayer2 = 1; // Set player state to jump
            if (!wasBelow)
            {
                wasBelow = true;
                lastBelowTime = currentTime;

                if (currentTime - lastAboveTime <= timeWindow && bullets > 0 && CanGenerateWave())
                {
                    // Onda de agacharse con tecla �2�
                    GameObject wave = Instantiate(wavePrefabcroach, wavePosition, rotation);
                    WaveCollider wavescript = wave.GetComponent<WaveCollider>();
                    Debug.Log("Wavescript: " + wavescript);
                    if (sensorObject.name == "Player1")
                    {
                        wavescript.setoriginal_player(true);
                    }
                    else
                    {
                        wavescript.setoriginal_player(false);
                    }
                    updatebulletstoManager();
                    PlaySound(crouchSound, wavePosition); // Play crouch sound
                    UpdateLastWaveTime();
                }
            }
        }
        else
        {
            wasBelow = false;
        }
        if (!wasBelow && !wasAbove)
        {
            if (sensorObject.name == "Player1")
                GameManager.Instance.stateplayer1 = 0; // Set player state to jump
            else
                GameManager.Instance.stateplayer2 = 0; // Set player state to jump
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

        int getbulletsfromManager()
        {
            if (sensorObject.name == "Player1")
                return GameManager.Instance.bullet_play1; //get bullets from manager
            else
                return GameManager.Instance.bullet_play2;
            return 0;
        }

        void updatebulletstoManager()
        {
            Debug.Log("Update bullets to manager");
            bullets = bullets - 1;
            if (sensorObject.name == "Player1")
                GameManager.Instance.bullet_play1 = bullets; //get bullets from manager
            else
                GameManager.Instance.bullet_play2 = bullets;
        }
        bool CanGenerateWave()
        {
            if (sensorObject.name == "Player1")
                return Time.time - lastWaveTimePlayer1 >= waveCooldown;
            else
                return Time.time - lastWaveTimePlayer2 >= waveCooldown;
        }
        void UpdateLastWaveTime()
        {
            if (sensorObject.name == "Player1")
                lastWaveTimePlayer1 = Time.time;
            else
                lastWaveTimePlayer2 = Time.time;
        }


    }
}
