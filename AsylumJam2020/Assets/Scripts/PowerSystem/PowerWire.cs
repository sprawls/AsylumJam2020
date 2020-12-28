using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class PowerWire : Powerable
{
    [SerializeField] private int _floorID = -1;
    [SerializeField] private GameObject _cableVisionObject = default;

    private Polyline _polylineCable;
    private bool _wireActivated = false;

    public int FloorID { get => _floorID; }

    public void ActivateWire() {
        _polylineCable.gameObject.SetActive(true);
    }

    public void DeactivateWire() {
        _polylineCable.gameObject.SetActive(false);
    }

    protected override void Update() {
        base.Update();

        if(_wireActivated) {
            for(int i = 0; i < _polylineCable.Count; ++i) {
                PolylinePoint point = _polylineCable[i];
                float distance = Vector3.Distance(point.point + transform.position, ScryptPlayer.PlayerPosition);
                point.thickness = Mathf.Lerp(0f, WireManager.DEFAULT_WIRE_THICKNESS, (WireManager.WIRE_VISIBLE_RANGE - distance) / WireManager.WIRE_VISIBLE_RANGE);
            }
            
        }
    }

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
