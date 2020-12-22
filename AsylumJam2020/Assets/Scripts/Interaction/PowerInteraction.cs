using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerInteraction : InteractionBase {
   
    [SerializeField] private List<PowerSource> _linkedPowerSources = default;
    [SerializeField] private Renderer _cableVisionRenderer = default;

    #region ACCESSORS



    #endregion

    #region LIFECYCLE

    protected override void Awake() {
        base.Awake();
    }

    #endregion


    #region CALLBACK

    public override void OnPoweredOn() {
        base.OnPoweredOn();

        if (_cableVisionRenderer != null) {
            _cableVisionRenderer.material.color = CableVisioner.Powered_Color;
        }
    }

    public override void OnPoweredOff() {
        base.OnPoweredOff();

        if (_cableVisionRenderer != null) {
            _cableVisionRenderer.material.color = CableVisioner.Unpowered_Color;
        }
    }

    public override void Callback_OnHighlightedStart() {
        base.Callback_OnHighlightedStart();
    }

    public override void Callback_OnHighlightedStop() {
        base.Callback_OnHighlightedStop();
    }

    public override void Callback_OnInteracted() {
        base.Callback_OnInteracted();

        foreach(PowerSource source in _linkedPowerSources) {
            source.TogglePower();
        }
    }

    #endregion

}
