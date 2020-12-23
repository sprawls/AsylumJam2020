using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerInteraction : InteractionBase {
   
    [SerializeField] private List<PowerSource> _linkedPowerSources = default;
    [SerializeField] private Renderer _cableVisionRenderer = default;

    [SerializeField] private bool _isActive = false;

    #region ACCESSORS



    #endregion

    #region LIFECYCLE

    protected override void Awake() {
        base.Awake();
    }

    #endregion

    #region LOGIC

    private void TogglePowerOnSource() {
        foreach (PowerSource source in _linkedPowerSources) {
            source.TogglePower();
        }
    }

    #endregion


    #region CALLBACK

    public override void OnPoweredOn() {
        base.OnPoweredOn();

        if (_cableVisionRenderer != null) {
            _cableVisionRenderer.material.color = CableVisioner.Powered_Color;
        }

        if (_isActive) {
            TogglePowerOnSource();
        }
    }

    public override void OnPoweredOff() {
        base.OnPoweredOff();

        if (_cableVisionRenderer != null) {
            _cableVisionRenderer.material.color = CableVisioner.Unpowered_Color;
        }

        if(_isActive) {
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

            _isActive = !_isActive;
            TogglePowerOnSource();
        }

    }

   

    #endregion

}
