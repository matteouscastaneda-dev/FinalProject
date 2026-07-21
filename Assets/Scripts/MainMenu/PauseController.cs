using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference pauseAction;

    [Header("Panels")]
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private GameObject settingsPanel;

    [Header("Disable script While Paused")]
    [SerializeField] private CameraController cameraController;

    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName = "00_MainMenu";

    private bool isPaused;

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        pauseAction.action.performed += OnPausePressed;
        pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        pauseAction.action.performed -= OnPausePressed;
        pauseAction.action.Disable();
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Escape toggles paused
    /// </summary>
    private void OnPausePressed(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    /// <summary>
    /// Paused shows the overlay and freezes gameplay
    /// </summary>
    private void Pause()
    {
        isPaused = true;
        pauseOverlay.SetActive(true);
        settingsPanel.SetActive(false);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (cameraController != null)
        {
            cameraController.enabled = false;
        }
    }

    /// <summary>
    /// Exits paused 
    /// hides both panels and unfreezes
    /// </summary>
    public void Resume()
    {
        isPaused = false;
        pauseOverlay.SetActive(false);
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraController != null)
        {
            cameraController.enabled = true;
        }
    }

    /// <summary>
    /// Opens the settings panel on top of the pause overlay
    /// </summary>
    public void OnSettingsClicked()
    {
        pauseOverlay.SetActive(false);
        settingsPanel.SetActive(true);
    }

    /// <summary>
    /// Closes settings and returns to the pause overlay
    /// </summary>
    public void OnSettingsBackClicked()
    {
        settingsPanel.SetActive(false);
        pauseOverlay.SetActive(true);
    }

    /// <summary>
    /// Unfreezes and loads the main menu scene
    /// </summary>
    public void OnReturnToMenuClicked()
    {
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.Instance.LoadScene(mainMenuSceneName);
    }
}