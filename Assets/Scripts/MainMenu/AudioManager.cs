using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Volume")]
    [Range(0f, 1f)][SerializeField] private float masterVolume = 1f;
    [Range(0f, 1f)][SerializeField] private float musicVolume = 1f;
    [Range(0f, 1f)][SerializeField] private float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ApplyGlobalVolume();
    }

    /// <summary>
    /// Sets volume to the current master
    /// </summary>
    private void ApplyGlobalVolume()
    {
        AudioListener.volume = masterVolume;
    }
    public float MasterVolume
    {
        get { return masterVolume; }
        set
        {
            masterVolume = Mathf.Clamp01(value);
            ApplyGlobalVolume();
        }
    }

    public float MusicVolume
    {
        get { return musicVolume; }
        set { musicVolume = Mathf.Clamp01(value); }
    }

    public float SfxVolume
    {
        get { return sfxVolume; }
        set { sfxVolume = Mathf.Clamp01(value); }
    }
}