using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class InteractionBase : Powerable
{
    public event Action<bool> OnInteractionTriggered;

    [HideInInspector] public UnityEvent Event_Highlighted_Start;
    [HideInInspector] public UnityEvent Event_Highlighted_Stop;
    [HideInInspector] public UnityEvent Event_Interaction_On;
    [HideInInspector] public UnityEvent Event_Interaction_Off;

    public enum InteractionState {
        NotHighlighted = 0,
        Highlighted = 1
    }

    private InteractionState _currentState;

    private static string ANIM_PARAM_HIGHLIGHT = "Highlighted";
    private static string ANIM_PARAM_USE = "Used";
    private static string ANIM_PARAM_USE_FAIL = "UsedFail";
    private static int ANIM_PARAM_HIGHLIGHT_HASH = -1;
    private static int ANIM_PARAM_USE_HASH = -1;
    private static int ANIM_PARAM_USE_FAIL_HASH = -1;

    #region ACCESSORS

    public InteractionState InteState { 
        get => _currentState;
        private set { 
            _currentState = value;
        }
    }

    #endregion

    #region LIFECYCLE

    protected override void Awake() {
        base.Awake();

        ANIM_PARAM_HIGHLIGHT_HASH = Animator.StringToHash(ANIM_PARAM_HIGHLIGHT);
        ANIM_PARAM_USE_HASH = Animator.StringToHash(ANIM_PARAM_USE);
        ANIM_PARAM_USE_FAIL_HASH = Animator.StringToHash(ANIM_PARAM_USE_FAIL);
    }

    #endregion


    #region CALLBACK

    public virtual void Callback_OnHighlightedStart() {
        InteState = InteractionState.Highlighted;

        if (Animator != null) {
            Animator.SetBool(ANIM_PARAM_HIGHLIGHT_HASH, true);

            Event_Highlighted_Start.Invoke();
        }
    }

    public virtual void Callback_OnHighlightedStop() {
        InteState = InteractionState.NotHighlighted;

        if (Animator != null) {
            Animator.SetBool(ANIM_PARAM_HIGHLIGHT_HASH, false);

            Event_Highlighted_Stop.Invoke();
        }
    }

    public virtual void Callback_OnInteracted() {
        if(Powered) {
            if (Animator != null) {
                Animator.SetTrigger(ANIM_PARAM_USE_HASH);
            }

            //Play Success SFX here
            Event_Interaction_On.Invoke();

            if(OnInteractionTriggered != null) OnInteractionTriggered.Invoke(true);
        } else {
            if (Animator != null) {
                Animator.SetTrigger(ANIM_PARAM_USE_FAIL_HASH);
            }

            //Play Fail SFX here
            Event_Interaction_Off.Invoke();

            if (OnInteractionTriggered != null) OnInteractionTriggered.Invoke(false);
        }

    }

    #endregion

}
