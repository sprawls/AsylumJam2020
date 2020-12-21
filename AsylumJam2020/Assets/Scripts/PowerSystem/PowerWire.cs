using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWire : Powerable
{

    [SerializeField] private GameObject _cableVisionObject = default;

    #region OVERRIDE

    public override void OnPoweredOn() {
        _cableVisionObject.SetActive(true);
    }

    public override void OnPoweredOff() {
        _cableVisionObject.SetActive(false);
    }

    #endregion
}
