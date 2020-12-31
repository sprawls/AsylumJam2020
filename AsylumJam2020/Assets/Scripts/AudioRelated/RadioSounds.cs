using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadioSounds : MonoBehaviour
{
    AudioSource MyAudioSource;
    public BasicAudioEventPlayer basicAudioEventPlayer;
    InteractionBase interactionBase;
    public bool isPlaying;

    void Start()
    {
        basicAudioEventPlayer.audio.loop = true;
        interactionBase = this.transform.parent.GetComponent<InteractionBase>();
        interactionBase.Event_Interaction_On.AddListener(SelectStateOfSound);      
    }

    void Update()
    {
        
    }

    void StopLoop()
    {
        MyAudioSource.loop = false;
        MyAudioSource.Stop();
    }

    void SelectStateOfSound()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            basicAudioEventPlayer.PlayInteractionOnEvent.Invoke();
            basicAudioEventPlayer.PlayLoop();
        }
        else
        {
            isPlaying = false;
            StartCoroutine(StopRadioSound());
        }
    }
    IEnumerator StopRadioSound()
    {
        basicAudioEventPlayer.PlayInteractionOffEvent.Invoke();
        yield return new WaitForSeconds(0.25f);
        basicAudioEventPlayer.audio.Stop();    
    }

}
