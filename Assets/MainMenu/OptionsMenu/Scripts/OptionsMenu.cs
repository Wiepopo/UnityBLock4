using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections.Generic;

public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Slider volumeSlider;

    private Resolution[] resolutions;

    // Pending values
    private int pendingResolutionIndex;
    private bool pendingFullscreen;
    private float pendingVolume;

    void Start()
    {
        resolutions = Screen.resolutions;
        List<Resolution> uniqueResolutions = new List<Resolution>();

        foreach (var res in resolutions)
        {
            if (!uniqueResolutions.Any(r => r.width == res.width && r.height == res.height))
            {
                uniqueResolutions.Add(res);
            }
        }

        resolutions = uniqueResolutions.ToArray();

        resolutionDropdown.ClearOptions();

        int currentResIndex = 0;
        var options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string resString = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(resString);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        // Load saved settings if available
        int savedResIndex = PlayerPrefs.GetInt("resIndex", currentResIndex);
        bool savedFullscreen = PlayerPrefs.GetInt("fullscreen", Screen.fullScreen ? 1 : 0) == 1;
        float savedVolume = PlayerPrefs.GetFloat("volume", AudioListener.volume);

        resolutionDropdown.value = savedResIndex;
        resolutionDropdown.RefreshShownValue();
        fullscreenToggle.isOn = savedFullscreen;
        volumeSlider.value = savedVolume;

        // Set pending
        pendingResolutionIndex = savedResIndex;
        pendingFullscreen = savedFullscreen;
        pendingVolume = savedVolume;

        // Apply immediately
        ApplySettings();
    }

    public void OnResolutionChanged(int index)
    {
        pendingResolutionIndex = index;
    }

    public void OnFullscreenToggled(bool isFullscreen)
    {
        pendingFullscreen = isFullscreen;
    }

    public void OnVolumeChanged(float volume)
    {
        pendingVolume = volume;
    }

    public void ApplySettings()
    {
        Resolution selectedRes = resolutions[pendingResolutionIndex];

        Screen.SetResolution(selectedRes.width, selectedRes.height, pendingFullscreen);
        Screen.fullScreen = pendingFullscreen;
        AudioListener.volume = pendingVolume;

        // Save to PlayerPrefs
        PlayerPrefs.SetInt("resIndex", pendingResolutionIndex);
        PlayerPrefs.SetInt("fullscreen", pendingFullscreen ? 1 : 0);
        PlayerPrefs.SetFloat("volume", pendingVolume);
        PlayerPrefs.Save();

        Debug.Log($"Applied & Saved: {selectedRes.width}x{selectedRes.height} | Fullscreen: {pendingFullscreen} | Volume: {pendingVolume}");
    }
}
