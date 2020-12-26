using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInteraction : InteractionBase
{
    [SerializeField] private bool _stateActive = false;

    [SerializeField] private bool _powerBased = false;

    private static string ANIM_PARAM_STATE = "StateActive";
    private static int ANIM_PARAM_STATE_HASH = -1;

    #region LIFECYCLE

    protected override void Awake() {
        base.Awake();

        ANIM_PARAM_STATE_HASH = Animator.StringToHash(ANIM_PARAM_STATE);
        UpdateAnimatorState();
    }

    #endregion

    private void UpdateAnimatorState() {
        if (Animator != null) {
            Animator.SetBool(ANIM_PARAM_STATE_HASH, _stateActive);
            Animator.SetBool("IsOpenFront", _stateActive);
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

            _stateActive = !_stateActive;

            UpdateAnimatorState();
        }

    }

    public override void OnPoweredOn() {
        base.OnPoweredOn();

        if(_powerBased) {
            _stateActive = true;
        }
    }

    public override void OnPoweredOff() {
        base.OnPoweredOff();

        if (_powerBased) {
            _stateActive = false;
        }
    }


    #endregion
}
