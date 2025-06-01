using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public GameObject[] hearts;
    public Material redMaterial;
    public Material blackMaterial;
    public AudioClip loseLifeSound; // Clip de sonido al perder vida

    private int lives = 3;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            hearts[lives].GetComponent<MeshRenderer>().material = blackMaterial;

            // Reproducir sonido de pérdida de vida
            if (loseLifeSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(loseLifeSound);
            }
        }
    }

    public void ResetHearts()
    {
        lives = 3;
        foreach (GameObject heart in hearts)
        {
            heart.GetComponent<MeshRenderer>().material = redMaterial;
        }
    }
}




