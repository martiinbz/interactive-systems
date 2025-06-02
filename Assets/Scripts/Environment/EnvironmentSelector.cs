using UnityEngine;

public class EnvironmentSelector : MonoBehaviour
{
    public GameObject forestTheme;
    public GameObject desertTheme;
    public GameObject snowTheme;

    public GameObject[] themeButtons; // Assign the 3 physical buttons
    public GameObject menuButton;     // Assign the menu button object

    void Start()
    {
        ShowThemeButtons(); // Initial state: show the 3 buttons
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SelectForest();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SelectMenu();
        }

    }

    public void SelectForest()
    {
        DisableAllThemes();
        forestTheme.SetActive(true);
        GameManager.Instance.ResetGame(); 
        SwitchToMenuButton();
    }

    public void SelectDesert()
    {
        DisableAllThemes();
        desertTheme.SetActive(true);
        GameManager.Instance.ResetGame();
        SwitchToMenuButton();
    }

    public void SelectSnow()
    {
        DisableAllThemes();
        snowTheme.SetActive(true);
        GameManager.Instance.ResetGame();
        SwitchToMenuButton();
    }

    public void SelectMenu()
    {
        DisableAllThemes();
        ShowThemeButtons();
    }

    public void SwitchToMenuButton()
    {
        foreach (GameObject button in themeButtons)
            button.SetActive(false);

        menuButton.SetActive(true);
    }

    public void ShowThemeButtons()
    {
        foreach (GameObject button in themeButtons)
            button.SetActive(true);

        menuButton.SetActive(false);
        GameManager.Instance.ResetGame();
        GameManager.Instance.unablebuttonsround();
    }

    void DisableAllThemes()
    {
        forestTheme.SetActive(false);
        desertTheme.SetActive(false);
        snowTheme.SetActive(false);
    }
}