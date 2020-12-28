using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EntityFollowingBehaviour : MonoBehaviour
{
    BasicAudioEventPlayer basicAudioEventPlayer;
    [SerializeField]AudioSource entityFollowingAudioSource;
    GameObject player;
    [SerializeField] Transform playerhead;
    public Vector3 SourceOffset;

    [SerializeField] float timeRemaining = 10;
    public float MinTime;
    public float MaxTime;

    public bool FadeComplete;
    public AudioMixerGroup AudioGroup;

    private const float MuteValue = -80.0f;

    void Awake()
    {
        basicAudioEventPlayer = GetComponent<BasicAudioEventPlayer>();
;       entityFollowingAudioSource = basicAudioEventPlayer.GetComponent<AudioSource>();
        SourceOffset = new Vector3(0, -0.5f, -8f);
    }

    void Start()
    {
        MinTime = 5f;
        MaxTime = 20f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerhead = player.GetComponentInChildren<AudioListener>().transform.parent;
    }

    void Update()
    {
        this.transform.position = playerhead.position + SourceOffset;

        RandomTimedEventCall();
    }

    void RandomTimedEventCall()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            ChangeEntitySourceParam();
            ResetTimerRandom();
        }
    }

    void ResetTimerRandom()
    {
        float randNumb = UnityEngine.Random.Range(MinTime, MaxTime);
        timeRemaining = randNumb;
    }

    void ChangeEntitySourceParam()
    {
        float randNumb = UnityEngine.Random.Range(0.2f, 0.8f);
        StartCoroutine(FadeOut(AudioGroup.audioMixer, "EntityFollowerVolume", 20));

        ChangeEntitySourceVolume();
        ChangeEntitySourcePitch();
        ChangeEntitySourcePan();

        StartCoroutine(FadeIn(AudioGroup.audioMixer, "EntityFollowerVolume", 20));

    }
    void ChangeEntitySourceVolume()
    {
        float randNumb = UnityEngine.Random.Range(0.2f, 0.8f);
        entityFollowingAudioSource.volume = randNumb;
    }
    void ChangeEntitySourcePitch()
    {
        float randNumb = UnityEngine.Random.Range(0.7f, 1f);
        entityFollowingAudioSource.pitch = randNumb;
    }
    void ChangeEntitySourcePan()
    {
        float randNumb = UnityEngine.Random.Range(-1f, 1f);
        entityFollowingAudioSource.panStereo = randNumb;
    }

    IEnumerator FadeOut(AudioMixer audioGroup, string paramName, float FadeTime)
    {
        FadeComplete = false;
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
        FadeComplete = true;
    }

    IEnumerator FadeIn(AudioMixer audioGroup, string paramName, float FadeTime)
    {
        FadeComplete = false;
        float actualVolume;
        float volumeTofade;

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
        FadeComplete = true;
    }
}