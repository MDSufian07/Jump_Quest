using System.IO;
using UnityEngine;
using UnityEngine.Audio;

namespace SaveSystem
{
   public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public SettingsData CurrentSettings { get; private set; }

    [Header("Audio")] // 
    public AudioMixer audioMixer; 

    private string _settingsPath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _settingsPath = Path.Combine(Application.persistentDataPath, "Settings.json");
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSettings()
    {
        if (File.Exists(_settingsPath))
        {
            string jsonString = File.ReadAllText(_settingsPath);
            CurrentSettings = JsonUtility.FromJson<SettingsData>(jsonString);
        }
        else
        {
            CurrentSettings = new SettingsData();
            SaveSettings();
        }

        ApplySettings();
    }

    public void SaveSettings()
    {
        string jsonString = JsonUtility.ToJson(CurrentSettings);
        File.WriteAllText(_settingsPath, jsonString);
    }

    public void ApplySettings()
    {
        // Apply graphics
        QualitySettings.SetQualityLevel(CurrentSettings.graphicsQuality);
        switch (CurrentSettings.graphicsQuality)
        {
            case 0: Screen.SetResolution(1280, 720, true, CurrentSettings.refreshRate); break;
            case 1: Screen.SetResolution(1600, 900, true, CurrentSettings.refreshRate); break;
            case 2: Screen.SetResolution(1920, 1080, true, CurrentSettings.refreshRate); break;
            case 3: Screen.SetResolution(2560, 1440, true, CurrentSettings.refreshRate); break;
        }
        Debug.Log($"[Settings] Applying volumes - Music: {CurrentSettings.musicVolume}, SFX: {CurrentSettings.sfxVolume}");
        SetAudioVolume("MusicVolume", CurrentSettings.musicVolume);
        SetAudioVolume("SFXVolume", CurrentSettings.sfxVolume);
    }

    public void UpdateVolume(float music, float sfx)
    {
        CurrentSettings.musicVolume = music;
        CurrentSettings.sfxVolume = sfx;

        SetAudioVolume("MusicVolume", music);
        SetAudioVolume("SFXVolume", sfx);

        SaveSettings();
    }

    private void SetAudioVolume(string exposedParam, float volume)
    {
        // Convert 0.0001–1 slider value to dB (-80 to 0)
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(exposedParam, dB);
    }

    public void UpdateGraphics(int qualityIndex)
    {
        CurrentSettings.graphicsQuality = qualityIndex;
        ApplySettings();
        SaveSettings();
    }

    public void UpdateRefreshRate(int refreshRate)
    {
        CurrentSettings.refreshRate = refreshRate;
        ApplySettings();
        SaveSettings();
    }
}

}
