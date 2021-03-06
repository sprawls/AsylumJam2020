using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[RequireComponent(typeof(AudioSource), typeof(SphereCollider), typeof(BoxCollider))]
public class BasicAudioEventPlayer : MonoBehaviour
{
    [SerializeField]new internal AudioSource audio;

    public bool LoopIsNotInterupted;

    [SerializeField] InteractionBase soundInteractionSource;
    [SerializeField] PowerInteraction powerInteractionSource;
    public bool PowerInteractionType;
    [SerializeField] StateInteraction stateInteractionSource;
    public bool StateInteractionType;

    bool canSearchInteraction;
    bool listenersOn;
    private string _playerAudioTag = "PlayerAudio";

    [Space(20)]
    public bool RoomAmbiantType;
    public bool ObjectAmbiantType;
    public bool InteractionType;
    public bool MixedType;
    public bool OtherType;
    public bool OtherTypeLoop;

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
    public AudioClip[] InteractionONSounds;
    public AudioClip[] InteractionOffSounds;
    public AudioClip[] InteractionFailSounds;

    [Space(5)]
    public AudioClip[] OtherSounds;

    [Space(20)]
    public UnityEvent PlayAudio;
    public UnityEvent StopAudio;
    public UnityEvent PlayRandomAudio;
    public UnityEvent PlayHoverEvent;
    public UnityEvent PlayHoverOffEvent;
    public UnityEvent PlayInteractionOnEvent;
    public UnityEvent PlayInteractionOffEvent;

    delegate void SoundsMethod();
    List<SoundsMethod> soundMethod = new List<SoundsMethod>();

    void Awake()
    {
        audio = GetComponent<AudioSource>();

        PlayerDetectionBoxCollider = GetComponent<BoxCollider>();
        PlayerDetectionSphereCollider = GetComponent<SphereCollider>();
        PlayerDetectionBoxCollider.isTrigger = true;
        PlayerDetectionSphereCollider.isTrigger = true;

        SetAudioSourceInitialValue();
        ConfigureAudioSetUp();
    }

    void Start()
    {
        CreateList();
    }

    void Update()
    {
        InteractWithSoundObject();
    }

    public void InteractWithSoundObject()
    {
        if (!canSearchInteraction) return;
        
        if (InteractionType || MixedType || OtherType || OtherTypeLoop)
        {
            if (soundInteractionSource != null && !listenersOn)//(interactionRaycaster.CurrentInteraction != null && !listenersOn)
            {
                Invoke("AddInteractionsListeners", 0f);
            }
        }
    }

    public void AddInteractionsListeners()
    {
        listenersOn = true;
        if (soundInteractionSource != null)
        {
            soundInteractionSource.Event_Highlighted_Start.AddListener(PlayHoverSound);
            soundInteractionSource.Event_Highlighted_Stop.AddListener(PlayHoverOffSound);
            soundInteractionSource.Event_Interaction_On.AddListener(PlaySelectedInteractionSound);
            soundInteractionSource.Event_Interaction_Off.AddListener(PlaySelectedInteractionSound);
            soundInteractionSource.Event_Interaction_Failed.AddListener(PlayInteractionFailedSound);

            Debug.Log("listeners Added");
        }
    }
    public void RemoveInteractionsListeners()
    {
        listenersOn = false;
        if (soundInteractionSource != null)
        {
            soundInteractionSource.Event_Highlighted_Start.RemoveListener(PlayHoverSound);
            soundInteractionSource.Event_Highlighted_Stop.RemoveListener(PlayHoverOffSound);
            soundInteractionSource.Event_Interaction_On.RemoveListener(PlaySelectedInteractionSound);
            soundInteractionSource.Event_Interaction_Off.RemoveListener(PlaySelectedInteractionSound);
            soundInteractionSource.Event_Interaction_Failed.RemoveListener(PlayInteractionFailedSound);

            Debug.Log("listeners Removed");
        }
    }

    public void CheckSoundBanks()
    {
        if (HoverSounds != null || HoverOffSounds != null || InteractionONSounds != null || InteractionOffSounds != null || InteractionFailSounds != null)
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
        if (!LoopIsNotInterupted)
        {
            audio.Pause();
        }
    }

    public void SetAudioSourceInitialValue()
    {
        audio.spatialBlend = 1f;
        audio.minDistance = 0.05f;
        maxDistanceToHear = audio.maxDistance;
        audio.mute = true;
        audio.Pause();
    }

    public void GetSpecificInteractionType()
    {
        if (soundInteractionSource.GetComponent<PowerInteraction>() != null)
        {
            powerInteractionSource = soundInteractionSource.GetComponent<PowerInteraction>();
            PowerInteractionType = true;
        }
        if (soundInteractionSource.GetComponent<StateInteraction>() != null)
        {
            stateInteractionSource = soundInteractionSource.GetComponent<StateInteraction>();
            StateInteractionType = true;
        }
    }

    public void ConfigureAudioSetUp()
    {
        CheckSoundBanks();

        if (AmbiantSound != null && RoomAmbiantType)
        {
            PlayerDetectionSphereCollider.enabled = false;
            PlayerDetectionBoxCollider.enabled = true;

            PlayerDetectionBoxCollider.isTrigger = true;
            PlayerDetectionBoxCollider.size = new Vector3(maxDistanceToHear + 5f, maxDistanceToHear + 5f, PlayerDetectionBoxCollider.size.z);

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

            if (this.transform.parent.GetComponent<InteractionBase>() != null)
            {
                soundInteractionSource = this.transform.parent.GetComponent<InteractionBase>();
            }

            if (soundInteractionSource.GetComponent<PowerInteraction>() != null)
            {
                powerInteractionSource = soundInteractionSource.GetComponent<PowerInteraction>();
            }

            GetSpecificInteractionType();
        }
        else if (AmbiantSound != null && MixedType)
        {
            PlayerDetectionBoxCollider.enabled = false;
            PlayerDetectionSphereCollider.enabled = true;

            PlayerDetectionSphereCollider.radius = maxDistanceToHear + 5f;

            AmbiantAudioInit();

            if (this.transform.parent.GetComponent<InteractionBase>() != null)
            {
                soundInteractionSource = this.transform.parent.GetComponent<InteractionBase>();
            }

            GetSpecificInteractionType();
        }
        else if (OtherType)
        {
            PlayerDetectionBoxCollider.enabled = false;
            PlayerDetectionSphereCollider.enabled = true;

            PlayerDetectionSphereCollider.radius = maxDistanceToHear + 5f;
            audio.loop = false;
            audio.playOnAwake = false;
        }
        else if (OtherTypeLoop)
        {
            PlayerDetectionBoxCollider.enabled = false;
            PlayerDetectionSphereCollider.enabled = true;

            PlayerDetectionSphereCollider.radius = maxDistanceToHear + 5f;
            audio.playOnAwake = false;

            if (this.transform.parent.GetComponent<InteractionBase>() != null)
            {
                soundInteractionSource = this.transform.parent.GetComponent<InteractionBase>();
            }
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
        if (other.gameObject.tag == _playerAudioTag)
        {
            canSearchInteraction = true;

            audio.mute = false;
            if (!LoopIsNotInterupted)
            {
                audio.UnPause();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == _playerAudioTag)
        {
            RemoveInteractionsListeners();

            canSearchInteraction = false;

            if (!LoopIsNotInterupted)
            {
                audio.Pause();
            }

            audio.mute = true;
        }
    }

    void CreateList()
    {
        soundMethod.Add(PlayHoverSound);
        soundMethod.Add(PlayHoverOffSound);
        soundMethod.Add(PlayInteractionOffSound);
        soundMethod.Add(PlayInteractionOnSound);
        soundMethod.Add(PlayInteractionFailedSound);
    }

    // Highlight d'un objet
    public void PlayHoverSound()
    {
        if (HoverSounds.Length != 0)
        {
            int randNumb = UnityEngine.Random.Range(0, HoverSounds.Length);
            audio.PlayOneShot(HoverSounds[randNumb]);
        }
    }
    public void PlayHoverOffSound()
    {
        if (HoverOffSounds.Length != 0)
        {
            int randNumb = UnityEngine.Random.Range(0, HoverOffSounds.Length);
            audio.PlayOneShot(HoverOffSounds[randNumb]);
        }
    }

    // Pour interaction
    public void PlayInteractionOffSound()
    {
        Debug.Log("Interaction OFF");
        if (InteractionOffSounds.Length != 0)
        {
            int randNumb = UnityEngine.Random.Range(0, InteractionOffSounds.Length);
            audio.PlayOneShot(InteractionOffSounds[randNumb]);
        }
    }
    public void PlayInteractionOnSound()
    {
        Debug.Log("Interaction ON");
        if (InteractionONSounds.Length != 0)
        {
            int randNumb = UnityEngine.Random.Range(0, InteractionONSounds.Length);
            audio.PlayOneShot(InteractionONSounds[randNumb]);
        }
    }
    public void PlayInteractionFailedSound()
    {
        Debug.Log("Interaction Failed");
        if (InteractionFailSounds.Length != 0)
        {
            int randNumb = UnityEngine.Random.Range(0, InteractionFailSounds.Length);
            audio.PlayOneShot(InteractionFailSounds[randNumb]);
        }
    }

    public void PlaySelectedInteractionSound()
    {
        if (PowerInteractionType)
        {
            if (powerInteractionSource.IsActive)
            {
                soundMethod[2]();
            }
            else
            {
                soundMethod[3]();
            }
        }
        if (StateInteractionType)
        {
            if (stateInteractionSource.StateActive)
            {
                soundMethod[2]();
            }
            if (!stateInteractionSource.StateActive)
            {
                soundMethod[3]();
            }
            if(!stateInteractionSource._powerBased && !stateInteractionSource.Powered)
            {
                soundMethod[4]();
            }
        }
    }

    //Ambiance
    public void StartAmbiantSound()
    {
        
    }
    public void StopAmbiantSound()
    {

    }

    //RandomPlayer
    public void PlayRandomSounds()
    {
        int randNumb = UnityEngine.Random.Range(0, OtherSounds.Length);
        audio.PlayOneShot(OtherSounds[randNumb]);
    }

    public void PlayLoop()
    {
        int randNumb = UnityEngine.Random.Range(0, OtherSounds.Length);
        audio.clip = OtherSounds[randNumb];
        audio.Play(0);
    }
}
