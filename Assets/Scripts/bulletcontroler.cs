using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcontroler : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.name == "Player1")
            {
                GameManager.Instance.bullet_play1 = 1 + GameManager.Instance.bullet_play1;
                Debug.Log("Player1 picked up a bullet! Total bullets: " + GameManager.Instance.bullet_play1);
            }
            else
            {
                GameManager.Instance.bullet_play2 = 1 + GameManager.Instance.bullet_play2;
            }
            Destroy(gameObject);
        }
    }
 }
