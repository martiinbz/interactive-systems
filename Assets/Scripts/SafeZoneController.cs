using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneController : MonoBehaviour
{
    public float fadeinTime = 5f; // Time to fade in
    public float lifeTime = 10f; // Time to live after fade in
    public float fadeOutTime = 5f; // Time to fade out

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

    IEnumerator FadeInOut()
    {
        yield return StartCoroutine(Fade(0,1,fadeinTime));
        col.enabled = true;

        yield return new WaitForSeconds(lifeTime);

        col.enabled = false;

        yield return StartCoroutine(Fade(1,0,fadeOutTime));

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
}
