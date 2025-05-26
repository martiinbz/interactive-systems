using UnityEngine;

public class EnvironmentSelector : MonoBehaviour
{
    public GameObject forestTheme;
    public GameObject desertTheme;
    public GameObject snowTheme;

    public GameObject themeSelectionUI;

    private bool menuVisible = true;

    void Update()
    {
        // Pulsar T para mostrar/ocultar el menú
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleUI();
        }
    }

    public void SelectForest()
    {
        DisableAllThemes();
        forestTheme.SetActive(true);
        HideUI();
    }

    public void SelectDesert()
    {
        DisableAllThemes();
        desertTheme.SetActive(true);
        HideUI();
    }

    public void SelectSnow()
    {
        DisableAllThemes();
        snowTheme.SetActive(true);
        HideUI();
    }

    void DisableAllThemes()
    {
        forestTheme.SetActive(false);
        desertTheme.SetActive(false);
        snowTheme.SetActive(false);
    }

    void HideUI()
    {
        themeSelectionUI.SetActive(false);
        menuVisible = false;
    }

    void ToggleUI()
    {
        menuVisible = !menuVisible;
        themeSelectionUI.SetActive(menuVisible);
    }
}