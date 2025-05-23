using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance;

        public SettingsData CurrentSettings { get; private set; }

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
            string jsonString = JsonUtility.ToJson(CurrentSettings, true);
            File.WriteAllText(_settingsPath, jsonString);
        }

        public void ApplySettings()
        {
            QualitySettings.SetQualityLevel(CurrentSettings.graphicsQuality);
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;

            switch (CurrentSettings.graphicsQuality)
            {
                case 0:
                    Screen.SetResolution(1280, 720, true, CurrentSettings.refreshRate);
                    break;
                case 1:
                    Screen.SetResolution(1600, 900, true, CurrentSettings.refreshRate);
                    break;
                case 2:
                    Screen.SetResolution(1920, 1080, true, CurrentSettings.refreshRate);
                    break;
                case 3:
                    Screen.SetResolution(2560, 1440, true, CurrentSettings.refreshRate);
                    break;
            }

            Debug.Log("Resolution: " + Screen.currentResolution.width + "x" + Screen.currentResolution.height + " @ " + Screen.currentResolution.refreshRate + "Hz");
            Debug.Log("Target Refresh Rate Set: " + CurrentSettings.refreshRate);
            Debug.Log($"Applied Graphics Quality: {CurrentSettings.graphicsQuality}");
            Debug.Log($"Applied Refresh Rate: {CurrentSettings.refreshRate}");
            Debug.Log($"Resolution Set: {Screen.width}x{Screen.height}, FullScreen: {Screen.fullScreen}");

        }

        public void UpdateVolume(float music, float sfx)
        {
            CurrentSettings.musicVolume = music;
            CurrentSettings.sfxVolume = sfx;
            ApplySettings();
            SaveSettings();
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
