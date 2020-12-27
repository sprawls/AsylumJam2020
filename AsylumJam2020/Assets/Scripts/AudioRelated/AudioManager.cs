using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private const float MuteValue = -80.00f;

    [Header("Ambiance")]
    public AudioMixerGroup AmbianceMixer;
    public float AmbianceVolume;
    public bool AmbianceMute;

    [Header("SFX")]
    public AudioMixerGroup SFXMixer;
    public float SFXVolume;
    public bool SFXMute;

    [Header("Music")]
    public AudioMixerGroup MusicMixer;
    public float MusicVolume;
    public bool MusicMute;
    public AudioMixerGroup[] MusicTracks;

    void Start()
    {
        SetMixerVolumeParam();
    }

    void Update()
    {
        SetMixerVolumeParam();
    }

    public void SetMixerVolumeParam()
    {
        //Ambiance
        if (AmbianceMute)
        {
            AmbianceMixer.audioMixer.SetFloat("AmbiantVolumeParam", MuteValue);
        }
        else
        {
            AmbianceMixer.audioMixer.SetFloat("AmbiantVolumeParam", AmbianceVolume);
        }

        //SFX
        if (SFXMute)
        {
            AmbianceMixer.audioMixer.SetFloat("SFXVolumeParam", MuteValue);
        }
        else
        {
            SFXMixer.audioMixer.SetFloat("SFXVolumeParam", SFXVolume);
        }

        //Music
        if (MusicMute)
        {
            AmbianceMixer.audioMixer.SetFloat("MusicVolumeParam", MuteValue);
        }
        else
        {
            MusicMixer.audioMixer.SetFloat("MusicVolumeParam", MusicVolume);
        }      
    }
}
