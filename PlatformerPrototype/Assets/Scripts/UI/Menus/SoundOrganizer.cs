using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Effect,
    Voice,
    Ambient,
    Music
}

public class SoundOrganizer : MonoBehaviour
{
    [SerializeField]
    private AudioSource source = null;
    [SerializeField]
    private SoundType soundType = SoundType.Effect;
    [SerializeField]
    private float volumeMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SetSourceVolume(RetrieveSoundValue());
        SubscribeToCategory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float RetrieveSoundValue()
    {
        switch(soundType)
        {
            case SoundType.Effect:
                return SoundSettings.EffectsVolume;
            case SoundType.Voice:
                return SoundSettings.VoiceVolume;
            case SoundType.Ambient:
                return SoundSettings.AmbientVolume;
            case SoundType.Music:
                return SoundSettings.MusicVolume;
            default:
                return 1f;
        }
    }

    private void SubscribeToCategory()
    {
        switch(soundType)
        {
            case SoundType.Effect:
                SoundSettings.AddToEffectSources(this);
                break;
            case SoundType.Voice:
                SoundSettings.AddToVoiceSources(this);
                break;
            case SoundType.Ambient:
                SoundSettings.AddToAmbientSources(this);
                break;
            case SoundType.Music:
                SoundSettings.AddToMusicSources(this);
                break;
            default:
                SoundSettings.AddToEffectSources(this);
                break;
        }
    }

    public void SetSourceVolume(float value)
    {
        source.volume = (value * volumeMultiplier);
    }
}
