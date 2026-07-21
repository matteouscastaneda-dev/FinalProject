using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Scene Names")]
    [SerializeField] private string firstSceneName = "03_Demo";

    private void Start()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    /// <summary>
    /// Loads the first scene
    /// </summary>
    public void OnStartMeetingClicked()
    {
        GameManager.Instance.LoadScene(firstSceneName);
    }

    /// <summary>
    /// Shows the settings panel and hides the main panel
    /// </summary>
    public void OnSettingsClicked()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    /// <summary>
    /// Returns to the main panel
    /// </summary>
    public void OnSettingsBackClicked()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    /// <summary>
    /// Exits the build or stops play mode
    /// </summary>
    public void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}