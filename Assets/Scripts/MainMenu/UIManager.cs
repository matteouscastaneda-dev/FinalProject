using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Overlays")]
    [SerializeField] private GameObject loadingScreen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }
    }

    /// <summary>
    /// Activates the loading screen
    /// </summary>
    public void ShowLoadingScreen()
    {
        if (loadingScreen == null)
        {
            return;
        }
        loadingScreen.SetActive(true);
    }

    /// <summary>
    /// Deactivates the loading screen
    /// </summary>
    public void HideLoadingScreen()
    {
        if (loadingScreen == null)
        {
            return;
        }
        loadingScreen.SetActive(false);
    }
}