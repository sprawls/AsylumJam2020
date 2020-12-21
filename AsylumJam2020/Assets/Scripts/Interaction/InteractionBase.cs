using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InteractionBase : MonoBehaviour
{
    public enum InteractionState {
        Inactive = 0,
        Highlighted = 1,
        Active = 2,
    }

    [SerializeField] private Animator _animator;

    private InteractionState _currentState;

    private static string ANIM_PARAM_HIGHLIGHT = "Highlighted";
    private static string ANIM_PARAM_USE = "Used";
    private static int ANIM_PARAM_HIGHLIGHT_HASH = -1;
    private static int ANIM_PARAM_USE_HASH = -1;

    #region ACCESSORS

    public InteractionState CurrentState { 
        get => _currentState;
        private set { 
            _currentState = value;
        }
    }

    #endregion

    #region LIFECYCLE

    protected virtual void Awake() {
        ANIM_PARAM_HIGHLIGHT_HASH = Animator.StringToHash(ANIM_PARAM_HIGHLIGHT);
        ANIM_PARAM_USE_HASH = Animator.StringToHash(ANIM_PARAM_USE);
    }

    #endregion


    #region CALLBACK

    public virtual void Callback_OnHighlightedStart() {
        if(_animator != null) {
            _animator.SetBool(ANIM_PARAM_HIGHLIGHT_HASH, true);
        }
    }

    public virtual void Callback_OnHighlightedStop() {
        if (_animator != null) {
            _animator.SetBool(ANIM_PARAM_HIGHLIGHT_HASH, false);
        }
    }

    public virtual void Callback_OnInteracted() {
        if (_animator != null) {
            _animator.SetTrigger(ANIM_PARAM_USE_HASH);
        }
    }

    #endregion

}
