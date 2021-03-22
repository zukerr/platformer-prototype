using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundSettings : MonoBehaviour
{
    private static float effectsVolume = 1;
    public static float EffectsVolume
    {
        get
        {
            return effectsVolume;
        }
        private set
        {
            effectsVolume = value;
            SetupSources(value, effectSources);
        }
    }
    private static float voiceVolume = 1;
    public static float VoiceVolume
    {
        get { return voiceVolume; }
        private set
        {
            voiceVolume = value;
            SetupSources(value, voiceSources);
        }
    }
    private static float ambientVolume = 1;
    public static float AmbientVolume
    {
        get { return ambientVolume; }
        private set
        {
            ambientVolume = value;
            SetupSources(value, ambientSources);
        }
    }
    private static float musicVolume = 1;
    public static float MusicVolume
    {
        get { return musicVolume; }
        private set
        {
            musicVolume = value;
            SetupSources(value, musicSources);
        }
    }

    [SerializeField]
    private GameObject rootGameObject = null;

    [SerializeField]
    private Toggle masterVolumeToggle = null;
    [SerializeField]
    private Slider masterVolumeSlider = null;
    [SerializeField]
    private TextMeshProUGUI masterVolumeSliderValueText = null;

    [SerializeField]
    private Toggle soundEffectsToggle = null;
    [SerializeField]
    private Slider effectsVolumeSlider = null;
    [SerializeField]
    private TextMeshProUGUI effectsVolumeSliderValueText = null;
    [SerializeField]
    private Slider voiceVolumeSlider = null;
    [SerializeField]
    private TextMeshProUGUI voiceVolumeSliderValueText = null;
    [SerializeField]
    private Slider ambientVolumeSlider = null;
    [SerializeField]
    private TextMeshProUGUI ambientVolumeSliderValueText = null;

    [SerializeField]
    private Toggle musicToggle = null;
    [SerializeField]
    private Slider musicVolumeSlider = null;
    [SerializeField]
    private TextMeshProUGUI musicVolumeSliderValueText = null;

    private float masterVolumeMutedValue;
    private float effectsVolumeMutedValue;
    private float voiceVolumeMutedValue;
    private float ambientVolumeMutedValue;
    private float musicVolumeMutedValue;

    private bool tempSoundEnabled;
    private bool tempEffectsEnabled;
    private bool tempMusicEnabled;

    private float tempMasterVolume;
    private float tempEffectsVolume;
    private float tempVoiceVolume;
    private float tempAmbientVolume;
    private float tempMusicVolume;

    private static List<SoundOrganizer> effectSources = new List<SoundOrganizer>();
    private static List<SoundOrganizer> voiceSources = new List<SoundOrganizer>();
    private static List<SoundOrganizer> ambientSources = new List<SoundOrganizer>();
    private static List<SoundOrganizer> musicSources = new List<SoundOrganizer>();

    public void SetupStartingValues()
    {
        masterVolumeSlider.value = 20;
        OnMasterVolumeSlider();

        effectsVolumeSlider.value = 30;
        OnEffectsVolumeSlider();

        voiceVolumeSlider.value = 70;
        OnVoiceVolumeSlider();
    }

    // Start is called before the first frame update
    void Start()
    {
        masterVolumeMutedValue = AudioListener.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddToEffectSources(SoundOrganizer soundOrganizer)
    {
        effectSources.Add(soundOrganizer);
    }

    public static void AddToVoiceSources(SoundOrganizer soundOrganizer)
    {
        voiceSources.Add(soundOrganizer);
    }

    public static void AddToAmbientSources(SoundOrganizer soundOrganizer)
    {
        ambientSources.Add(soundOrganizer);
    }

    public static void AddToMusicSources(SoundOrganizer soundOrganizer)
    {
        musicSources.Add(soundOrganizer);
    }

    private static void SetupSources(float value, List<SoundOrganizer> sources)
    {
        foreach (SoundOrganizer so in sources)
        {
            if (so != null)
            {
                so.SetSourceVolume(value);
            }
        }
    }

    private void OnEnable()
    {
        tempSoundEnabled = masterVolumeToggle.isOn;
        tempEffectsEnabled = soundEffectsToggle.isOn;
        tempMusicEnabled = musicToggle.isOn;

        tempMasterVolume = masterVolumeSlider.value;
        tempEffectsVolume = effectsVolumeSlider.value;
        tempVoiceVolume = voiceVolumeSlider.value;
        tempAmbientVolume = ambientVolumeSlider.value;
        tempMusicVolume = musicVolumeSlider.value;
    }

    public void CancelChanges()
    {
        masterVolumeToggle.isOn = tempSoundEnabled;
        soundEffectsToggle.isOn = tempEffectsEnabled;
        musicToggle.isOn = tempMusicEnabled;

        masterVolumeSlider.value = tempMasterVolume;
        effectsVolumeSlider.value = tempEffectsVolume;
        voiceVolumeSlider.value = tempVoiceVolume;
        ambientVolumeSlider.value = tempAmbientVolume;
        musicVolumeSlider.value = tempMusicVolume;

        ToggleSound();
        ToggleEffects();
        ToggleMusic();

        OnMasterVolumeSlider();
        OnEffectsVolumeSlider();
        OnVoiceVolumeSlider();
        OnAmbientVolumeSlider();
        OnMusicVolumeSlider();

        rootGameObject.SetActive(false);
        //PlayerControls.soundSettingsOn = false;
    }

    public void ApplyAndClose()
    {
        rootGameObject.SetActive(false);
        //PlayerControls.soundSettingsOn = false;
    }

    public void ToggleSound()
    {
        bool value = masterVolumeToggle.isOn;
        if(!value)
        {
            masterVolumeMutedValue = AudioListener.volume;
            AudioListener.volume = 0f;
            masterVolumeSlider.interactable = false;
        }
        else
        {
            masterVolumeSlider.interactable = true;
            AudioListener.volume = masterVolumeMutedValue;
        }
    }

    public void ToggleEffects()
    {
        bool value = soundEffectsToggle.isOn;
        if(!value)
        {
            effectsVolumeMutedValue = SoundSettings.EffectsVolume;
            voiceVolumeMutedValue = SoundSettings.VoiceVolume;
            ambientVolumeMutedValue = SoundSettings.AmbientVolume;

            SoundSettings.EffectsVolume = 0f;
            SoundSettings.VoiceVolume = 0f;
            SoundSettings.AmbientVolume = 0f;

            effectsVolumeSlider.interactable = false;
            voiceVolumeSlider.interactable = false;
            ambientVolumeSlider.interactable = false;
        }
        else
        {
            effectsVolumeSlider.interactable = true;
            voiceVolumeSlider.interactable = true;
            ambientVolumeSlider.interactable = true;

            SoundSettings.EffectsVolume = effectsVolumeMutedValue;
            SoundSettings.VoiceVolume = voiceVolumeMutedValue;
            SoundSettings.AmbientVolume = ambientVolumeMutedValue;
        }
    }

    public void ToggleMusic()
    {
        bool value = musicToggle.isOn;
        if (!value)
        {
            musicVolumeMutedValue = SoundSettings.MusicVolume;

            SoundSettings.MusicVolume = 0f;

            musicVolumeSlider.interactable = false;
        }
        else
        {
            musicVolumeSlider.interactable = true;

            SoundSettings.MusicVolume = musicVolumeMutedValue;
        }
    }

    public void OnMasterVolumeSlider()
    {
        AudioListener.volume = masterVolumeSlider.value / 100;
        masterVolumeMutedValue = AudioListener.volume;
        masterVolumeSliderValueText.text = (masterVolumeMutedValue * 100).ToString();
    }

    public void OnEffectsVolumeSlider()
    {
        Slider slider = effectsVolumeSlider;
        TextMeshProUGUI valueText = effectsVolumeSliderValueText;
        SoundSettings.EffectsVolume = slider.value / 100;
        valueText.text = (SoundSettings.EffectsVolume * 100).ToString();
    }

    public void OnVoiceVolumeSlider()
    {
        Slider slider = voiceVolumeSlider;
        TextMeshProUGUI valueText = voiceVolumeSliderValueText;
        SoundSettings.VoiceVolume = slider.value / 100;
        valueText.text = (SoundSettings.VoiceVolume * 100).ToString();
    }

    public void OnAmbientVolumeSlider()
    {
        Slider slider = ambientVolumeSlider;
        TextMeshProUGUI valueText = ambientVolumeSliderValueText;
        SoundSettings.AmbientVolume = slider.value / 100;
        valueText.text = (SoundSettings.AmbientVolume * 100).ToString();
    }

    public void OnMusicVolumeSlider()
    {
        Slider slider = musicVolumeSlider;
        TextMeshProUGUI valueText = musicVolumeSliderValueText;
        SoundSettings.MusicVolume = slider.value / 100;
        valueText.text = (SoundSettings.MusicVolume * 100).ToString();
    }
}
