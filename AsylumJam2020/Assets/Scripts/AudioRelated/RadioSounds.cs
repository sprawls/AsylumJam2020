using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadioSounds : MonoBehaviour
{
    AudioSource MyAudioSource;
    public BasicAudioEventPlayer basicAudioEventPlayer;
    InteractionBase interactionBase;

    void Start()
    {
        basicAudioEventPlayer.audio.loop = true;
        interactionBase = this.transform.parent.GetComponent<InteractionBase>();
        //interactionBase.Event_Interaction_On.AddListener(basicAudioEventPlayer.PlayLoop);
        //interactionBase.Event_Interaction_Off.AddListener(basicAudioEventPlayer.StopLoop);
    }

    void Update()
    {
        
    }
}
