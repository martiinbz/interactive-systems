using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneController : MonoBehaviour
{
    public float fadeinTime = 5f; // Time to fade in
    public float lifeTime = 10f; // Time to live after fade in
    public float fadeOutTime = 5f; // Time to fade out

    public AudioSource audioSource; // Referencia al AudioSource existente
    public AudioClip appearSound;
    public AudioClip disappearSound;
    public AudioClip enterSound;
    public AudioClip disenterSound;

    private Renderer rend;
    private Collider col;
    private Material mat;

    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        mat = rend.material;
        SetAlpha(0);
        col.enabled = false;

        StartCoroutine(FadeInOut());
    }
    void Update()
    {
        if (!GameManager.Instance.roundActive)
        {
            Destroy(gameObject);
        }

    }

    IEnumerator FadeInOut()
    {
        audioSource.PlayOneShot(appearSound, 1.0f);
        yield return StartCoroutine(Fade(0,1,fadeinTime));
        col.enabled = true;

        yield return new WaitForSeconds(lifeTime);

        col.enabled = false;
        audioSource.PlayOneShot(disappearSound, 1.0f);
        yield return StartCoroutine(Fade(1,0,fadeOutTime));
        GameManager.Instance.unsafe_player("Player1");
        GameManager.Instance.unsafe_player("Player2");
        Destroy(gameObject);
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            float a = Mathf.Lerp(from, to, t / duration);
            SetAlpha(a);
            t += Time.deltaTime;
            yield return null;
        }
        SetAlpha(to);
    }

    void SetAlpha(float a)
    {
        if (mat.HasProperty("_Color"))
        {
            Color c = mat.color;
            c.a = a;
            mat.color = c;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("SafeZone:OnTriggerEnter: " + other.gameObject.name);
        if (other.gameObject.name == "Player1")
        {
            audioSource.PlayOneShot(enterSound, 2.0f);
            GameManager.Instance.safe_player("Player1");
        }
        else if (other.gameObject.name == "Player2")
        {
            audioSource.PlayOneShot(enterSound, 2.0f);
            GameManager.Instance.safe_player("Player2");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("SafeZone:OnTriggerExit: " + other.gameObject.name);
        if (other.gameObject.name == "Player1")
        {
            audioSource.PlayOneShot(disenterSound, 2.0f);
            GameManager.Instance.unsafe_player("Player1");
        }
        else if (other.gameObject.name == "Player2")
        {
            audioSource.PlayOneShot(disenterSound, 2.0f);
            GameManager.Instance.unsafe_player("Player2");
        }
    }
}
