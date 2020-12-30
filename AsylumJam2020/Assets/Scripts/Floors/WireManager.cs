using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    private PowerWire[] _allWires;
    private List<PowerWire> _activatedWires;
    
    public static float WIRE_VISIBLE_RANGE = 5f;
    public static float DEFAULT_WIRE_THICKNESS = 1f;

    protected void Awake()
    {
        _activatedWires = new List<PowerWire>(8);
        RegisterAllWires();
    }

    protected void Start() {
        foreach (PowerWire wire in _allWires) {
            wire.DeactivateWire();
        }
    }

    private void OnEnable() {
        FloorManager.OnFloorChanged += Callback_OnFloorChanged;
    }

    private void OnDisable() {
        FloorManager.OnFloorChanged -= Callback_OnFloorChanged;
    }


    private void RegisterAllWires() {
        _allWires = FindObjectsOfType<PowerWire>(true);
    }

    private void ActivateWiresOfFloor(int floor) {
        foreach(PowerWire wire in _activatedWires) {
            wire.DeactivateWire();
        }

        _activatedWires = new List<PowerWire>(8);
        foreach (PowerWire wire in _allWires) {
            if (wire.FloorID == floor) {
                _activatedWires.Add(wire);
                wire.ActivateWire();
            }
        }

    }

    #region CALLBACK

    private void Callback_OnFloorChanged(int newFloor) {
        ActivateWiresOfFloor(newFloor);
    }


    #endregion

}
