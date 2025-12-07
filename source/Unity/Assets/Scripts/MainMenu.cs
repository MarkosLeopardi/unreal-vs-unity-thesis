using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public PlayerControls playerControls; // Reference to the PlayerControls script
    public GameObject settingsPanel; // Reference to the settings panel UI GameObject
    public GameObject MainMenuPanel; // Reference to the main menu panel UI GameObject
    public Button BackButton; // Assign in inspector
    public Button NoCrashButton; // Assign in inspector

    void Start()
    {
        playerControls = new PlayerControls();
        playerControls.UI.Enable();
        // Load saved quality setting if it exists
        int savedQuality = PlayerPrefs.GetInt("QualitySetting", -1);
        if (savedQuality != -1)
        {
            ChangeQuality(savedQuality);
        }

        // Load saved resolution setting if it exists
        int savedResolution = PlayerPrefs.GetInt("ResolutionSettings", -1);
        if (savedResolution != -1)
        {
            ChangeResolution(savedResolution);
        }
        settingsPanel.SetActive(false);
    }
    
    public void StartGame()
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        if(selectedObject.name == "NoCrash")
        {
            SceneManager.LoadScene("NoCrash");
        }
        else if(selectedObject.name == "Survival")
        {
            SceneManager.LoadScene("Survival");
        }
    }

    public void LoadSettings()
    {
        settingsPanel.SetActive(true);
        BackButton.Select();
        MainMenuPanel.SetActive(false);
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }

    public void ChangeQuality(int value)
    {
        // 0: Low, 1: Medium, 2: High
        switch (value)
        {
            case 0:
                QualitySettings.SetQualityLevel(0, true); // Low
                break;
            case 1:
                QualitySettings.SetQualityLevel(2, true); // Medium
                break;
            case 2:
                QualitySettings.SetQualityLevel(5, true); // High
                break;
            default:
                QualitySettings.SetQualityLevel(2, true); // Default to Medium
                break;
        }
        PlayerPrefs.SetInt("QualitySetting", value);
        PlayerPrefs.Save();
    }

    public void ChangeResolution(int value)
    {
        int width, height;
        switch (value)
        {
            case 0:
                width = 1280;
                height = 720;
                break; // 720p
            case 1:
                width = 1920;
                height = 1080;
                break; // 1080p
            case 2:
                width = 2560;
                height = 1440;
                break; // 1440p
            default:
                width = 1920;
                height = 1080;
                break; // Default to 1080p
        }
        Screen.SetResolution(width, height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionSettings", value);
        PlayerPrefs.Save();
    }

    public void Back()
    {
        settingsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        NoCrashButton.Select(); // Set focus back to the Start button
    }
}
