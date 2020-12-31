using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerInteraction : InteractionBase {
   
    [SerializeField] private List<PowerSource> _linkedPowerSources = default;
    [SerializeField] private Renderer _cableVisionRenderer = default;

    [SerializeField] private bool _isActive = false;

    private static string ANIM_PARAM_SwitchActive = "SwitchesActive";
    private static int ANIM_PARAM_SwitchActive_Hash = -1;


    #region ACCESSORS

    public bool IsActive { 
        get => _isActive;
        set { 
            _isActive = value;
            Animator.SetBool(ANIM_PARAM_SwitchActive_Hash, value);
        }
    }

    #endregion

    #region LIFECYCLE

    protected override void Awake() {
        ANIM_PARAM_SwitchActive_Hash = Animator.StringToHash(ANIM_PARAM_SwitchActive);

        base.Awake();
    }

    #endregion

    #region LOGIC

    private void TogglePowerOnSource() {
        foreach (PowerSource source in _linkedPowerSources) {
            source.TogglePowering();
        }
    }

    #endregion


    #region CALLBACK

    public override void OnPoweredOn() {
        base.OnPoweredOn();

        if (_cableVisionRenderer != null) {
            _cableVisionRenderer.material.color = CableVisioner.Powered_Color;
        }

        if (IsActive) {
            TogglePowerOnSource();
        }
    }

    public override void OnPoweredOff() {
        base.OnPoweredOff();

        if (_cableVisionRenderer != null) {
            _cableVisionRenderer.material.color = CableVisioner.Unpowered_Color;
        }

        if(IsActive) {
            TogglePowerOnSource();
        }
    }

    public override void Callback_OnHighlightedStart() {
        base.Callback_OnHighlightedStart();
    }

    public override void Callback_OnHighlightedStop() {
        base.Callback_OnHighlightedStop();
    }

    public override void Callback_OnInteracted() {
        if(Powered) {
            base.Callback_OnInteracted();

            IsActive = !IsActive;
            TogglePowerOnSource();
        }

    }

   

    #endregion

}
