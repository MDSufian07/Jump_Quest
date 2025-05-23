using System;
using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsUI : MonoBehaviour
    {
    
        public Slider musicSlider;
        public Slider sfxSlider;
        public TMP_Dropdown graphicsDropdown;
        public TMP_Dropdown refreshRateDropdown;
        void Start()
        {
            musicSlider.value = SettingsManager.instance.CurrentSettings.musicVolume;
            sfxSlider.value = SettingsManager.instance.CurrentSettings.sfxVolume;
            graphicsDropdown.value = SettingsManager.instance.CurrentSettings.graphicsQuality;
            refreshRateDropdown.value = SettingsManager.instance.CurrentSettings.refreshRate;
        }
        
        public void OnApplySettings()
        {
            float musicVolume = musicSlider.value;
            float sfxVolume = sfxSlider.value;
            int graphicsQuality = graphicsDropdown.value;
            int refreshRate = int.Parse(refreshRateDropdown.options[refreshRateDropdown.value].text);
        
            SettingsManager.instance.UpdateVolume(musicVolume, sfxVolume);
            SettingsManager.instance.UpdateGraphics(graphicsQuality);
            SettingsManager.instance.UpdateRefreshRate(refreshRate);
        }
    }
}
