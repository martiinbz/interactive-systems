using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcontroler : MonoBehaviour
{
    public AudioClip pickupSound; // Clip de sonido al recoger bala

    


    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.name == "Player1")
            {
                GameManager.Instance.bullet_play1++;
                Debug.Log("Player1 picked up a bullet! Total bullets: " + GameManager.Instance.bullet_play1);
            }
            else
            {
                GameManager.Instance.bullet_play2++;
            }

            // Reproducir sonido desde un objeto independiente
            
           
           
            

            Destroy(gameObject);
        }
    }
}

