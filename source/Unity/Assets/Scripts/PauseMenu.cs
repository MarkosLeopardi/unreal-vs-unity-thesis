using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // For InputAction and InputAction.CallbackContext
using UnityEngine.EventSystems;
using UnityEngine.UI; // For EventSystem

public class Pausemenu : MonoBehaviour
{
    public PlayerControls playerControls;
    [SerializeField] private GameObject pauseMenuUI; // Reference to the pause menu UI GameObject
    [SerializeField] private GameObject buttonHUD; // Reference to the button HUD GameObject]
    public bool isPaused = false; //flag to check if the game is paused
    public GameObject settingsPanel; // Reference to the settings panel
    public GameObject pausePanel; // Reference to the main menu panel
    public Button BackButton; // Reference to the back button in the settings panel
    public Button ContinueButton; // Reference to the continue button in the pause menu

    void Start()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
    }

    void Update()
    {
        if (playerControls.CarMovement.OpenMenu.WasCompletedThisFrame())
        {
            if (isPaused)
            {
                ResumeGame(); // Resume the game if it is paused
                playerControls.CarMovement.Enable(); // Re-enable car movement controls
            }
            else
            {
                PauseGame(); // Pause the game if it is not paused
                playerControls.UI.Enable(); // Enable UI controls for the pause menu
            }
        }
    }
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false); // Hide the pause menu UI
        buttonHUD.SetActive(true); // Show the button HUD
        Time.timeScale = 1f; // Resume the game
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true); // Show the pause menu UI
        buttonHUD.SetActive(false); // Hide the button HUD
        Time.timeScale = 0f; // Pause the game
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Ensure the game is running at normal speed
        // Load the main menu scene
        SceneManager.LoadScene("StartMenu");
    }
    public void LoadSettings()
    {
        settingsPanel.SetActive(true);
        BackButton.Select();
        pausePanel.SetActive(false);
    }
    public void Back()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
        ContinueButton.Select(); // Set focus back to the Start button
    }
    public void Restart()
    {
        Time.timeScale = 1f; // Ensure the game is running at normal speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
