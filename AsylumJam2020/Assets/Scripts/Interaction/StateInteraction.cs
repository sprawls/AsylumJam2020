using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInteraction : InteractionBase
{
    [SerializeField] private bool _stateActive = false;

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
        if (Powered) {
            base.Callback_OnInteracted();

            _stateActive = !_stateActive;

            UpdateAnimatorState();
        }

    }


    #endregion
}
