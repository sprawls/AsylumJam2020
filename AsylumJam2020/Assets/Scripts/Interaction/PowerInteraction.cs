using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerInteraction : InteractionBase {
   
    [SerializeField] private List<PowerSource> _linkedPowerSources = default;

    #region ACCESSORS



    #endregion

    #region LIFECYCLE

    protected override void Awake() {
        base.Awake();
    }

    #endregion


    #region CALLBACK

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
