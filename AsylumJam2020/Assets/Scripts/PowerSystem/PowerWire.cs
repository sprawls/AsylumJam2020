using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class PowerWire : Powerable
{

    [SerializeField] private GameObject _cableVisionObject = default;

    private Polyline _polylineCable;



    #region OVERRIDE

    protected override void Awake() {
        _polylineCable = _cableVisionObject.GetComponent<Polyline>();

        if (_polylineCable == null) {
            Debug.LogError("Cable has no polyline script !", gameObject);
        }

        base.Awake();
    }

    public override void OnPoweredOn() {
        _polylineCable.Color = CableVisioner.Powered_Color;
    }

    public override void OnPoweredOff() {
        _polylineCable.Color = CableVisioner.Unpowered_Color;
    }

    #endregion
}
