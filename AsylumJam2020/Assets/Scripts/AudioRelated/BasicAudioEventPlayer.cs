using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[RequireComponent(typeof(AudioSource), typeof(SphereCollider), typeof(BoxCollider))]
public class BasicAudioEventPlayer : MonoBehaviour
{
    new AudioSource audio;

    public bool RoomAmbiantType;
    public bool ObjectAmbiantType;
    public bool InteractionType;
    public bool MixedType;

    [Space(20)]
    public bool ReconfigureAudioSetUp;

    [Space(20)]
    public SphereCollider PlayerDetectionSphereCollider;
    public BoxCollider PlayerDetectionBoxCollider;
    float detectionRadius;
    float maxDistanceToHear;

    [Space(20)]
    public AudioClip ambiantSound;
    public AudioClip[] PonctualSounds;


    UnityEvent ReconfigureAudio;

    [Space(20)]
    public UnityEvent StartAudio;
    public UnityEvent StopAudio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();

        audio.spatialBlend = 1f;
        audio.minDistance = 0.05f;

        maxDistanceToHear = audio.maxDistance;

        PlayerDetectionBoxCollider = GetComponent<BoxCollider>();
        PlayerDetectionSphereCollider = GetComponent<SphereCollider>();
        PlayerDetectionBoxCollider.isTrigger = true;
        PlayerDetectionSphereCollider.isTrigger = true;

        ConfigureAudioSetUp();
    }

    void Start()
    {
        audio.mute = true;
        audio.Pause();
    }

    void Update()
    {
        if (ReconfigureAudioSetUp)
        {
            Invoke("ConfigureAudioSetUp",0.0f);
        }
    }

    public void AmbiantAudioInit()
    {
        audio.clip = ambiantSound;
        audio.loop = true;
        audio.Play();
        audio.Pause();
    }

    public void ConfigureAudioSetUp()
    {
        ReconfigureAudioSetUp = false;

        if (ambiantSound != null && RoomAmbiantType)
        {
            PlayerDetectionSphereCollider.enabled = false;
            PlayerDetectionBoxCollider.enabled = true;

            PlayerDetectionBoxCollider.isTrigger = true;
            PlayerDetectionBoxCollider.size = new Vector3(maxDistanceToHear + 5f, maxDistanceToHear + 5f, PlayerDetectionBoxCollider.size.y);

            AmbiantAudioInit();
        }
        else if (ambiantSound != null && ObjectAmbiantType)
        {
            PlayerDetectionBoxCollider.enabled = false;
            PlayerDetectionSphereCollider.enabled = true;

            PlayerDetectionSphereCollider.radius = maxDistanceToHear + 5f;

            AmbiantAudioInit();
        }
        else if (PonctualSounds != null && InteractionType)
        {
            PlayerDetectionBoxCollider.enabled = false;
            PlayerDetectionSphereCollider.enabled = true;

            PlayerDetectionSphereCollider.radius = maxDistanceToHear + 5f;
            audio.loop = false;
        }
        else if (ambiantSound != null && MixedType)
        {
            PlayerDetectionBoxCollider.enabled = false;
            PlayerDetectionSphereCollider.enabled = true;

            PlayerDetectionSphereCollider.radius = maxDistanceToHear + 5f;

            AmbiantAudioInit();
        }
        else
        {
            PlayerDetectionBoxCollider.enabled = false;
            PlayerDetectionSphereCollider.enabled = true;

            PlayerDetectionSphereCollider.radius = maxDistanceToHear + 5f;
            audio.mute = true;
            audio.Pause();
        }

        Debug.Log("AudioSource Reconfiguration Done");
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
