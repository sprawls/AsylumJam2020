using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class PowerWire : Powerable
{
    [SerializeField] private int _floorID = -1;
    
    private GameObject _cableVisionObject = default;
    private Polyline _polylineCable;
    private bool _wireActivated = false;

    public int FloorID { get => _floorID; }

    protected override void Initialize() {
        if (!IsInit) {
            _polylineCable = GetComponentInChildren<Polyline>(true);

            if (_polylineCable == null) {
                Debug.LogError("Cable has no polyline script !", gameObject);
            }

            _cableVisionObject = _polylineCable.gameObject;

            base.Initialize();
        }
    }


    public void ActivateWire() {
        if (!IsInit) Initialize();

        _polylineCable.gameObject.SetActive(true);
        _wireActivated = true;
    }

    public void DeactivateWire() {
        if (!IsInit) Initialize();

        _wireActivated = false;
        _polylineCable.gameObject.SetActive(false);
    }

    protected override void Update() {
        base.Update();

        if(_wireActivated) {
            for(int i = 0; i < _polylineCable.Count; ++i) {
                float distance = Vector3.Distance(_polylineCable[i].point + transform.position, ScryptPlayer.PlayerPosition);
                float thickness = Mathf.Lerp(0f, WireManager.DEFAULT_WIRE_THICKNESS, (WireManager.WIRE_VISIBLE_RANGE - distance) / WireManager.WIRE_VISIBLE_RANGE);
                _polylineCable.SetPointThickness(i, thickness);
            }
            
        }
    }

    #region OVERRIDE

    protected override void Awake() {
        base.Awake();
    }

    public override void OnPoweredOn() {
        base.OnPoweredOn();

        _polylineCable.Color = CableVisioner.Powered_Color;
    }

    public override void OnPoweredOff() {
        base.OnPoweredOff();

        _polylineCable.Color = CableVisioner.Unpowered_Color;
    }

    #endregion
}
