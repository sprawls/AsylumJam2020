using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInteraction : InteractionBase
{
    [SerializeField] private bool stateActive = false;
    [SerializeField] internal bool _powerBased = false;

    [Header("Revert")]
    [SerializeField, Tooltip("If >0, will revert to state inactive after X Seconds.")] 
    private float _revertOnTime = -1f;
    [SerializeField, Tooltip("If >0, will revert to state inactive when X Units away from player.")]
    private float _revertOnDistance = -1f;

    private float _timeActivated = 0f;

    //static
    private static string ANIM_PARAM_STATE = "StateActive";
    private static int ANIM_PARAM_STATE_HASH = -1;

    internal bool StateActive { 
        get => stateActive;
        set
        {
            stateActive = value;

            if (stateActive) _timeActivated = Time.time;
        }
    }

    #region LIFECYCLE

    protected override void Awake() {
        base.Awake();

        ANIM_PARAM_STATE_HASH = Animator.StringToHash(ANIM_PARAM_STATE);
        UpdateAnimatorState();
    }

    protected override void Update() {
        base.Update();

        if(stateActive) {
            if(_revertOnTime > 0f && Time.time > _timeActivated + _revertOnTime) {
                Callback_OnInteracted();
            } 
            else if(_revertOnDistance > 0f && Vector3.Distance(transform.position, ScryptPlayer.PlayerPosition) > _revertOnDistance) {
                Callback_OnInteracted();
            } 
        }
    }

    #endregion

    protected virtual void UpdateAnimatorState() {
        if (Animator != null) {
            Animator.SetBool(ANIM_PARAM_STATE_HASH, StateActive);
        }
    }

    #region CALLBACK

    public override void Callback_OnHighlightedStart() {
        base.Callback_OnHighlightedStart();
    }

    public override void Callback_OnHighlightedStop() {
        base.Callback_OnHighlightedStop();
    }

    public override void Callback_OnInteracted() {
        if (Powered && !_powerBased) {
            base.Callback_OnInteracted();

            StateActive = !StateActive;

            UpdateAnimatorState();
        }
        if (!Powered && !_powerBased)
        {
            Event_Interaction_Failed.Invoke();
        }
    }

    public override void OnPoweredOn() {
        base.OnPoweredOn();

        if(_powerBased) {
            StateActive = true;
        }
    }

    public override void OnPoweredOff() {
        base.OnPoweredOff();

        if (_powerBased) {
            StateActive = false;
        }
    }


    #endregion
}
