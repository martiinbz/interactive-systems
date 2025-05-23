using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public GameObject[] hearts;
    public Material redMaterial;
    public Material blackMaterial;

    private int lives = 3;

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            hearts[lives].GetComponent<MeshRenderer>().material = blackMaterial;
        }
    }

    public void ResetHearts()
    {
        lives = 3;
        foreach(GameObject heart in hearts)
        {
            heart.GetComponent<MeshRenderer>().material = redMaterial;
        }
    }
}
