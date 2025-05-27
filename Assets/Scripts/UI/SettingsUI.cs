using SaveSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class SettingsUI : MonoBehaviour
    {
        public Slider musicSlider;
        public Slider sfxSlider;
        public TMP_Dropdown graphicsDropdown;
        public TMP_Dropdown refreshRateDropdown;

        private void Start()
        {
            // Set UI from saved data
            musicSlider.value = SettingsManager.Instance.CurrentSettings.musicVolume;
            sfxSlider.value = SettingsManager.Instance.CurrentSettings.sfxVolume;
            graphicsDropdown.value = SettingsManager.Instance.CurrentSettings.graphicsQuality;

            // Populate refresh rates
            refreshRateDropdown.ClearOptions();
            var options = new TMP_Dropdown.OptionDataList();
            options.options.Add(new TMP_Dropdown.OptionData("60"));
            options.options.Add(new TMP_Dropdown.OptionData("75"));
            options.options.Add(new TMP_Dropdown.OptionData("120"));
            options.options.Add(new TMP_Dropdown.OptionData("144"));
            options.options.Add(new TMP_Dropdown.OptionData("165"));
            options.options.Add(new TMP_Dropdown.OptionData("240"));
            refreshRateDropdown.options = options.options;

            // Select saved refresh rate
            for (int i = 0; i < refreshRateDropdown.options.Count; i++)
            {
                if (int.Parse(refreshRateDropdown.options[i].text) == SettingsManager.Instance.CurrentSettings.refreshRate)
                {
                    refreshRateDropdown.value = i;
                    break;
                }
            }

            // Add real-time volume listeners
            musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }

        public void OnMusicVolumeChanged(float value)
        {
            SettingsManager.Instance.UpdateVolume(value, sfxSlider.value);
        }

        public void OnSFXVolumeChanged(float value)
        {
            SettingsManager.Instance.UpdateVolume(musicSlider.value, value);
        }

        public void OnApplySettings()
        {
            int graphicsQuality = graphicsDropdown.value;
            int refreshRate = int.Parse(refreshRateDropdown.options[refreshRateDropdown.value].text);
            SettingsManager.Instance.UpdateGraphics(graphicsQuality);
            SettingsManager.Instance.UpdateRefreshRate(refreshRate);
        }
    }
}
