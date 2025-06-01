using UnityEngine;

public class bulletcontroler : MonoBehaviour
{
    public AudioClip pickupSound; // Clip de sonido al recoger bala
    public AudioSource audioSource; // Referencia al AudioSource existente

    private void OnTriggerEnter(Collider other)
    {
        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound, 1.0f); // Reproducir sonido con volumen 1.0
        }
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

            // Iniciar Coroutine antes de desactivar
            StartCoroutine(DestroyAfterDelay(.5f));
        }
    }

    private System.Collections.IEnumerator DestroyAfterDelay(float delay)
    {
        // Esperar el retraso antes de desactivar y destruir
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); // Desactivar después del retraso
        Destroy(gameObject); // Destruir después de desactivar
    }
}