using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    #region Unity Public
    public GameObject SettingsPanel;
    public GameObject CreditPanel;

    public Dropdown QualityLevel;

    public Slider SensibilitySlider;
    public Slider GameSpeedSlider;

    public Slider EffectVolumeSlider;
    public Slider AmbientVolumeSlider;

    public AudioManager Audio;
    public MapRotation Map;
    #endregion

    void Start()
    {
        SettingsPanel.SetActive(false);
        CreditPanel.SetActive(false);
        ShowQualityValue();
        ShowSensibility();
        ShowSoundValue();
        Audio.SoundEffect.volume = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !CreditPanel.activeInHierarchy)
        {
            OpenCloseSettingsPanel();
        }

        if (Input.GetKeyDown(KeyCode.S) && CreditPanel.activeInHierarchy || Input.GetKeyDown(KeyCode.C) && SettingsPanel.activeInHierarchy)
        {
            OpenCloseCreditPanel();
        }
    }

    public void ShowSensibility()
    {
        SensibilitySlider.value = Map.MooveSensibility;
    }

    public void ApplySensibility()
    {
        Map.MooveSensibility = SensibilitySlider.value;
        Map.RotationSensibility = SensibilitySlider.value * 1.4f;
    }

    public void ShowSoundValue()
    {
        AmbientVolumeSlider.value = Audio.Ambient.volume;
        EffectVolumeSlider.value = Audio.SoundEffect.volume;
    }

    public void ApplySoundValue()
    {
        Audio.Ambient.volume = AmbientVolumeSlider.value;
        Audio.SoundEffect.volume = EffectVolumeSlider.value;
    }

    public void OpenCloseSettingsPanel()
    {
        if (!CreditPanel.activeInHierarchy)
        {
            if (SettingsPanel.activeInHierarchy) SettingsPanel.SetActive(false);
            else
            {
                SettingsPanel.SetActive(true);
                ShowQualityValue();
                ShowSensibility();
                ShowSoundValue();
            }
            Audio.ClickEffect();
        }
    }

    public void ShowQualityValue()
    {
        QualityLevel.value = QualitySettings.GetQualityLevel();
    }

    public void ApplyQualityValue()
    {
        QualitySettings.SetQualityLevel(QualityLevel.value, true);
    }

    public void OpenCloseCreditPanel()
    {
        if (!CreditPanel.activeInHierarchy) CreditPanel.SetActive(true);
        else CreditPanel.SetActive(false);
        Audio.ClickEffect();
    }
}
