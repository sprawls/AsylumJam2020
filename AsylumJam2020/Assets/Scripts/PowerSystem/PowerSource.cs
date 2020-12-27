using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : Powerable
{

    [SerializeField] private List<Powerable> _powerables;
    [SerializeField] private bool _isPowering = false;

    #region ACCESSORS

    public bool IsPowering { 
        get => _isPowering;
        set { 
            _isPowering = value; 
            UpdatePower(); 
        }
    }

    #endregion

    public override void OnPoweredOn() {
        base.OnPoweredOn();
        UpdatePower();
    }

    public override void OnPoweredOff() {
        base.OnPoweredOff();
        UpdatePower();
    }

    protected override void Awake() {
        base.Awake();
    }

    protected override void Start() {
        base.Start();

        UpdatePower();
    }

    private void UpdatePower() {
        foreach(Powerable powerable in _powerables) {
            if(powerable != null) {
                if (_isPowering && Powered) {
                    powerable.AddPowerSource(this);
                } else {
                    powerable.RemovePowerSource(this);
                }
            } else {
                Debug.LogError("Powerable is null in power source !", gameObject);
            }

        }
    }

    public void TogglePowering() {
        IsPowering = !IsPowering;
    }

}
