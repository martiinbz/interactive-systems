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


    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            hearts[lives].GetComponent<MeshRenderer>().material = blackMaterial;

            // Reproducir sonido de pérdida de vida
            if (loseLifeSound != null && audioSource != null)
            {
                Debug.Log("Reproduciendo sonido de pérdida de vida");
                audioSource.PlayOneShot(loseLifeSound,1.0f);
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

    public int GetLives()
    {
        return lives;
    }
}




