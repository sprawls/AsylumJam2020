using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : StateInteraction
{
    [Header("Door")]
    [SerializeField] private GameObject _cableVisionDoor = default;
    [SerializeField] private MeshRenderer _renderer = default;
    [SerializeField] private Transform _frontTransform;
    [SerializeField] private Transform _backTransform;


    private static string ANIM_PARAM_STATE_OPEN_FRONT = "IsOpenFront";
    private static string ANIM_PARAM_STATE_OPEN_BACK = "IsOpenBack";
    private static int ANIM_PARAM_STATE_OPEN_FRONT_HASH = -1;
    private static int ANIM_PARAM_STATE_OPEN_BACK_HASH = -1;

    #region LIFECYCLE

    protected override void Initialize() {
        if (!IsInit) {

            ANIM_PARAM_STATE_OPEN_FRONT_HASH = Animator.StringToHash(ANIM_PARAM_STATE_OPEN_FRONT);
            ANIM_PARAM_STATE_OPEN_BACK_HASH = Animator.StringToHash(ANIM_PARAM_STATE_OPEN_BACK);

            if (_cableVisionDoor != null) {
                if (AmountPowerSourceNeeded > 0) _cableVisionDoor.SetActive(true);
                else _cableVisionDoor.SetActive(false);
            }
            base.Initialize();
        }
    }

    protected override void Awake() {

        base.Awake();
    }

    #endregion

    private bool IsPlayerInFront() {
        float distFront = Vector3.Distance(_frontTransform.position, ScryptPlayer.PlayerPosition);
        float distBack = Vector3.Distance(_backTransform.position, ScryptPlayer.PlayerPosition);

        return distFront < distBack;
    }

    protected override  void UpdateAnimatorState() {
        base.UpdateAnimatorState();

        if (Animator != null) {
            if(StateActive) {
                if(IsPlayerInFront()) {
                    Animator.SetBool(ANIM_PARAM_STATE_OPEN_FRONT_HASH, true);
                } else {
                    Animator.SetBool(ANIM_PARAM_STATE_OPEN_BACK_HASH, true);
                }
            } else {
                Animator.SetBool(ANIM_PARAM_STATE_OPEN_FRONT_HASH, false);
                Animator.SetBool(ANIM_PARAM_STATE_OPEN_BACK_HASH, false);
            }
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
        base.Callback_OnInteracted();
    }

    public override void OnPoweredOn() {
        base.OnPoweredOn();

        Initialize();

        if (_cableVisionDoor != null) {
            _renderer.material.color = CableVisioner.Powered_Color;
        }
    }

    public override void OnPoweredOff() {
        base.OnPoweredOff();

        Initialize();

        if (_cableVisionDoor != null) {
            _renderer.material.color = CableVisioner.Unpowered_Color;
        }
    }


    #endregion
}
