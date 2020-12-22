using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[RequireComponent(typeof(AudioSource), typeof(SphereCollider))]
public class BasicAudioEventPlayer : MonoBehaviour
{
    new AudioSource audio;

    public int SelectedCasterType = 0;
    public bool AmbianceType;
    public bool InteractionType;
    public bool MixedType;

    [Space(20)]
    public SphereCollider PlayerDetectionCollider;
    float detectionRadius;
    float maxDistanceToHear;

    [Space(20)]
    public AudioClip ambiantSound;
    public AudioClip[] PonctualSounds;

    [Space(20)]
    public UnityEvent StartAudio;
    public UnityEvent StopAudio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        PlayerDetectionCollider = GetComponent<SphereCollider>();

        audio.spatialBlend = 1f;
        audio.minDistance = 0.05f;

        maxDistanceToHear = audio.maxDistance;
        PlayerDetectionCollider.radius = maxDistanceToHear + 5f;
    }

    void Start()
    {
        if (ambiantSound != null && AmbianceType)
        {
            audio.clip = ambiantSound;
            audio.loop = true;
            audio.Play();
            audio.Pause();
        }
        else if (PonctualSounds != null && InteractionType)
        {
            audio.loop = false;
        }
        else if (ambiantSound != null && MixedType)
        {
            audio.clip = ambiantSound;
            audio.loop = true;
            audio.Play();
            audio.Pause();
        }
        else
        {
            audio.mute = true;
            audio.Pause();
        }

        audio.mute = true;
        audio.Pause();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audio.mute = false;
            audio.UnPause();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audio.Pause();
            audio.mute = true;
        }
    } 

    // Highlight d'un objet
    public void PlayHoverSound()
    {

    }

    // Pour interaction
    public void PlayInteractionSound()
    {

    }

    //Ambiance
    public void StartAmbiantSound()
    {

    }
    public void StopAmbiantSound()
    {

    }
}
