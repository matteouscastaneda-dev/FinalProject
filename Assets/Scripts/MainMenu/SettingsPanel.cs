using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void OnEnable()
    {
        if (AudioManager.Instance == null)
        {
            return;
        }

        masterSlider.value = AudioManager.Instance.MasterVolume;
        musicSlider.value = AudioManager.Instance.MusicVolume;
        sfxSlider.value = AudioManager.Instance.SfxVolume;
    }

    /// <summary>
    /// Gives master slider value into AudioManager
    /// </summary>
    public void HandleMasterChanged(float value)
    {
        AudioManager.Instance.MasterVolume = value;
    }

    /// <summary>
    /// Gives the music slider value into AudioManager
    /// </summary>
    public void HandleMusicChanged(float value)
    {
        AudioManager.Instance.MusicVolume = value;
    }

    /// <summary>
    /// Gives the SFX slider value into AudioManager
    /// </summary>
    public void HandleSfxChanged(float value)
    {
        AudioManager.Instance.SfxVolume = value;
    }
}