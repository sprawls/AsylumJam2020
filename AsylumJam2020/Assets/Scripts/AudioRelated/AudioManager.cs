using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    private const float MuteValue = -80.00f;

    public int LayerOfSound;
    public bool IsInStairCase;

    public bool InGameSoundsSetting;

    [Header("Ambiance")]
    public AudioMixerGroup AmbianceMixer;
    public float AmbianceVolume;
    public bool AmbianceMute;

    [Header("SFX")]
    public AudioMixerGroup SFXMixer;
    public float SFXVolume;
    public bool SFXMute;
    public AudioMixerGroup ReverbSFXMixer;
    private const float initialReverbroomValue = -3891.00f;
    public float ReverbRoom;

    [Header("Music")]
    public AudioMixerGroup MusicMixer;
    public float MusicVolume;
    public bool MusicMute;
    [Space(5)]
    public AudioMixerGroup[] MusicTracks;

    [Header("Additional Controls")]
    public bool ResetVolumeLvl;

    [Space(5)]
    public float FadeSpeedMultiplier;

    [Space(5)]
    public bool FadeInTest;
    public bool FadeOutTest;
    bool fadeComplete;

    public UnityEvent Event_FadeInGameSound;
    public UnityEvent Event_FadeOutGameSound;
    public UnityEvent Event_ResetVolumeLvl;

    private void Awake()
    {
        SetMixerVolumeParam();
        FadeSpeedMultiplier = 10f;
    }
    private void Start()
    {

    }

    void Update()
    {
        LayerOfSound = FloorManager.CurrentFloor;
        IsInStairCase = FloorManager.IsInStaircase;
        SetReverbBusRoomLvl();

        if (InGameSoundsSetting)
        {
            SetMixerVolumeParam();
        }
        else
        {
            if (FadeInTest)
            {
                FadeInTest = false;
                Event_FadeInGameSound.Invoke();
            }

            if (FadeOutTest)
            {
                FadeOutTest = false;
                Event_FadeOutGameSound.Invoke();
            }
        }

        if (ResetVolumeLvl)
        {
            Event_ResetVolumeLvl.Invoke();
            ResetVolumeLvl = false;
        }

        //Pour escalier
        if (FloorManager.IsInStaircase)
        {
            ReverbRoom = initialReverbroomValue + 500.0f;
        }
        else
        {
            ReverbRoom = initialReverbroomValue;
        }
    }

    public void SetReverbBusRoomLvl()
    {
        ReverbSFXMixer.audioMixer.SetFloat("ReverbSFXReverbRoomParam", ReverbRoom);
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

    public void ResetMixersVolume()
    {
        AmbianceMixer.audioMixer.SetFloat("AmbiantVolumeParam", 0f);
        AmbianceVolume = 0f;

        SFXMixer.audioMixer.SetFloat("SFXVolumeParam", 0f);
        SFXVolume = 0f;

        MusicMixer.audioMixer.SetFloat("MusicVolumeParam", 0f);
        MusicVolume = 0f;
    }

    public void FadeInGameSound()
    {
        StartCoroutine(FadeIn(AmbianceMixer.audioMixer, "AmbiantVolumeParam", FadeSpeedMultiplier));
        StartCoroutine(FadeIn(SFXMixer.audioMixer, "SFXVolumeParam", FadeSpeedMultiplier));
        StartCoroutine(FadeIn(MusicMixer.audioMixer, "MusicVolumeParam", FadeSpeedMultiplier));

        Debug.Log("fadeIn");
    }
    public void FadeOutGameSound()
    {
        StartCoroutine(FadeOut(AmbianceMixer.audioMixer, "AmbiantVolumeParam", FadeSpeedMultiplier));
        StartCoroutine(FadeOut(SFXMixer.audioMixer, "SFXVolumeParam", FadeSpeedMultiplier));
        StartCoroutine(FadeOut(MusicMixer.audioMixer, "MusicVolumeParam", FadeSpeedMultiplier));
        Debug.Log("fadeout");
    }
  
    IEnumerator FadeOut(AudioMixer audioGroup, string paramName, float FadeTime)
    {
        fadeComplete = false;
        float actualVolume;
        float volumeTofade;

        audioGroup.GetFloat(paramName, out actualVolume);
        volumeTofade = actualVolume;

        while (volumeTofade > MuteValue)
        {
            audioGroup.SetFloat(paramName, volumeTofade -= (FadeTime * Time.deltaTime));

            yield return null;
        }

        audioGroup.SetFloat(paramName, MuteValue);

        if (volumeTofade != 0.0f)
        {
            audioGroup.SetFloat(paramName, MuteValue);
        }
        fadeComplete = true;
    }

    IEnumerator FadeIn(AudioMixer audioGroup, string paramName, float FadeTime)
    {
        fadeComplete = false;
        float actualVolume;
        float volumeTofade;

        audioGroup.SetFloat(paramName, MuteValue);

        audioGroup.GetFloat(paramName, out actualVolume);
        volumeTofade = actualVolume;

        while (volumeTofade < 0.0f)
        {
            audioGroup.SetFloat(paramName, volumeTofade += (FadeTime * Time.deltaTime));

            yield return null;
        }

        audioGroup.SetFloat(paramName, 0.0f);

        if (volumeTofade != 0.0f)
        {
            audioGroup.SetFloat(paramName, 0.0f);
        }
        fadeComplete = true;
    }
}
