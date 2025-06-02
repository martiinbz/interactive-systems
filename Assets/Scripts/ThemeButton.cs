using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeButton : MonoBehaviour
{
    public enum ThemeType { Forest, Desert, Snow, Menu}
    public ThemeType themeType;

    public EnvironmentSelector environmentSelector; // Assign in inspector
    public Renderer buttonRenderer;
    public Color baseColor = Color.white;
    public Color highlightColor = Color.yellow;
    public float selectionTime = 3f;

    public AudioClip hoverSound; // Clip de sonido al ponerse encima
    public AudioSource sceneAudioSource; // Referencia al AudioSource existente

    private float timer = 0f;
    private bool playerInside = false;
    private Material material;

    void Start()
    {
        if (buttonRenderer == null)
            buttonRenderer = GetComponent<Renderer>();

        material = buttonRenderer.material;
        material.color = baseColor;
    }

    void Update()
    {
        if (playerInside)
        {
            sceneAudioSource.PlayOneShot(hoverSound, 2.0f); // Reproducir sonido al entrar
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / selectionTime);

            // Blend color
            material.color = Color.Lerp(baseColor, highlightColor, t);

            if (timer >= selectionTime)
            {
                SelectTheme();
                ResetButton();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetButton();
        }
    }

    void ResetButton()
    {
        playerInside = false;
        timer = 0f;
        material.color = baseColor;
    }

    void SelectTheme()
    {
        switch (themeType)
        {
            case ThemeType.Forest:
                environmentSelector.SelectForest();
                break;
            case ThemeType.Desert:
                environmentSelector.SelectDesert();
                break;
            case ThemeType.Snow:
                environmentSelector.SelectSnow();
                break;
            case ThemeType.Menu:
                environmentSelector.SelectMenu();
                break;
        }
    }
}
