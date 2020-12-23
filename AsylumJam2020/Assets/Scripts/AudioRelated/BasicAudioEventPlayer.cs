using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[RequireComponent(typeof(AudioSource), typeof(SphereCollider), typeof(BoxCollider))]
public class BasicAudioEventPlayer : MonoBehaviour
{
    [SerializeField]new AudioSource audio;

    [SerializeField] InteractionBase SoundInteractionSource;

    bool canSearchInteraction;
    bool listenersOn;

    [Space(20)]
    public bool RoomAmbiantType;
    public bool ObjectAmbiantType;
    public bool InteractionType;
    public bool MixedType;

    [Space(20)]
    public SphereCollider PlayerDetectionSphereCollider;
    public BoxCollider PlayerDetectionBoxCollider;
    float detectionRadius;
    float maxDistanceToHear;

    [Space(5)]
    [Header("SoundBanks")]
    public bool PonctualSoundsEnabled;
    public AudioClip AmbiantSound;
    public AudioClip[] HoverSounds;
    public AudioClip[] HoverOffSounds;
    public AudioClip[] InteractionSounds;

    [Space(20)]
    public UnityEvent PlayAudio;
    public UnityEvent StopAudio;
    public UnityEvent PlayHoverEvent;
    public UnityEvent PlayHoverOffEvent;
    public UnityEvent PlayInteractionEvent;

    delegate void SoundsMethod();

    public bool Test;

    void Awake()
    {
        audio = GetComponent<AudioSource>();

        PlayerDetectionBoxCollider = GetComponent<BoxCollider>();
        PlayerDetectionSphereCollider = GetComponent<SphereCollider>();
        PlayerDetectionBoxCollider.isTrigger = true;
        PlayerDetectionSphereCollider.isTrigger = true;

        ConfigureAudioSetUp();

        SetAudioSourceInitialValue();
    }

    void Start()
    {
        audio.mute = true;
        audio.Pause();

        CreateList();
    }

    void Update()
    {
        InteractWithSoundObject();
    }

    public void InteractWithSoundObject()
    {
        if (!canSearchInteraction) return;
        
        if (InteractionType || MixedType)
        {
            if (SoundInteractionSource != null && !listenersOn)//(interactionRaycaster.CurrentInteraction != null && !listenersOn)
            {
                Invoke("AddInteractionsListeners", 0f);
            }
        }
    }

    public void AddInteractionsListeners()
    {
        listenersOn = true;
        if (SoundInteractionSource != null)
        {
            SoundInteractionSource.HighlightedStartEvent.AddListener(PlayHoverSound);
            SoundInteractionSource.HighlightedStopEvent.AddListener(PlayHoverOffSound);
            SoundInteractionSource.InteractionOkEvent.AddListener(PlayInteractionSound);

            Debug.Log("listeners Added");
        }
    }
    public void RemoveInteractionsListeners()
    {
        listenersOn = false;
        if (SoundInteractionSource != null)
        {
            SoundInteractionSource.HighlightedStartEvent.RemoveListener(PlayHoverSound);
            SoundInteractionSource.HighlightedStopEvent.RemoveListener(PlayHoverOffSound);
            SoundInteractionSource.InteractionOkEvent.RemoveListener(PlayInteractionSound);

            Debug.Log("listeners Removed");
        }
    }

    public void CheckSoundBanks()
    {
        if (HoverSounds != null || HoverOffSounds != null || InteractionSounds != null)
        {
            PonctualSoundsEnabled = true;
        }
        else
        {
            PonctualSoundsEnabled = false;
        }
    }

    public void AmbiantAudioInit()
    {
        audio.clip = AmbiantSound;
        audio.loop = true;
        audio.Play();
        audio.Pause();
    }

    public void SetAudioSourceInitialValue()
    {
        audio.spatialBlend = 1f;
        audio.minDistance = 0.05f;
        maxDistanceToHear = audio.maxDistance;
    }

    public void ConfigureAudioSetUp()
    {
        CheckSoundBanks();


        if (AmbiantSound != null && RoomAmbiantType)
        {
            PlayerDetectionSphereCollider.enabled = false;
            PlayerDetectionBoxCollider.enabled = true;

            PlayerDetectionBoxCollider.isTrigger = true;
            PlayerDetectionBoxCollider.size = new Vector3(maxDistanceToHear + 5f, maxDistanceToHear + 5f, PlayerDetectionBoxCollider.size.y);

            AmbiantAudioInit();
        }
        else if (AmbiantSound != null && ObjectAmbiantType)
        {
            PlayerDetectionBoxCollider.enabled = false;
            PlayerDetectionSphereCollider.enabled = true;

            PlayerDetectionSphereCollider.radius = maxDistanceToHear + 5f;

            AmbiantAudioInit();
        }
        else if (PonctualSoundsEnabled && InteractionType)
        {
            PlayerDetectionBoxCollider.enabled = false;
            PlayerDetectionSphereCollider.enabled = true;

            PlayerDetectionSphereCollider.radius = maxDistanceToHear + 5f;
            audio.loop = false;
            audio.playOnAwake = false;
            SoundInteractionSource = this.transform.parent.GetComponent<InteractionBase>();
        }
        else if (AmbiantSound != null && MixedType)
        {
            PlayerDetectionBoxCollider.enabled = false;
            PlayerDetectionSphereCollider.enabled = true;

            PlayerDetectionSphereCollider.radius = maxDistanceToHear + 5f;

            AmbiantAudioInit();
            SoundInteractionSource = this.transform.parent.GetComponent<InteractionBase>();
        }
        else
        {
            PlayerDetectionBoxCollider.enabled = false;
            PlayerDetectionSphereCollider.enabled = true;

            PlayerDetectionSphereCollider.radius = maxDistanceToHear + 5f;
            audio.mute = true;
            audio.Pause();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canSearchInteraction = true;

            audio.mute = false;
            audio.UnPause();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            RemoveInteractionsListeners();

            canSearchInteraction = false;

            audio.Pause();
            audio.mute = true;
        }
    }

    void CreateList()
    {
        List<SoundsMethod> soundMethod = new List<SoundsMethod>();
        soundMethod.Add(PlayHoverSound);
        soundMethod.Add(PlayHoverOffSound);
        soundMethod.Add(PlayInteractionSound);
    }

    // Highlight d'un objet
    public void PlayHoverSound()
    {
        audio.PlayOneShot(HoverSounds[0]);
    }
    public void PlayHoverOffSound()
    {
        audio.PlayOneShot(HoverOffSounds[0]);
    }

    // Pour interaction
    public void PlayInteractionSound()
    {
        audio.PlayOneShot(InteractionSounds[0]);
    }

    //Ambiance
    public void StartAmbiantSound()
    {

    }
    public void StopAmbiantSound()
    {

    }
}
