using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStompSoundBehaviour : MonoBehaviour
{
    BasicAudioEventPlayer basicAudioEventPlayer;
    GameObject player;
    [SerializeField]Transform playerhead;
    public Vector3 SourceOffset;

    [SerializeField] float timeRemaining = 10;
    public float MinTime;
    public float MaxTime;

    void Awake()
    {
        basicAudioEventPlayer = GetComponent<BasicAudioEventPlayer>();
        SourceOffset = new Vector3(0, 1.5f, 0);
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
            basicAudioEventPlayer.PlayRandomAudio.Invoke();
            ResetTimerRandom();
            Debug.Log("Entity is moving");
        }
    }

    void ResetTimerRandom()
    {
        float randNumb = UnityEngine.Random.Range(MinTime,MaxTime);
        timeRemaining = randNumb;
    }
}