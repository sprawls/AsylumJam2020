using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerable : MonoBehaviour
{

    [SerializeField] private int _amountPowerSourceNeeded = 1;
    [SerializeField] private Animator _powerableAnimator = default;

    public enum PowerState {
        Unpowered = 0,
        Powered = 1
    }

    private PowerState _powerState;
    private List<PowerSource> _currentPowerSources;

    private static string ANIM_PARAM_POWERED = "Powered";




    #region ACCESSORS

    public PowerState CurrentState { 
        get => _powerState;
        set { 
            if(_powerState != value) {
                _powerState = value;
                if (_powerState == PowerState.Powered) OnPoweredOn();
                else OnPoweredOff();
            }

        }
    }

    #endregion

    #region LIFECYCLE

    private void Awake() {
        _currentPowerSources = new List<PowerSource>(4);
        OnPoweredOff();
    }

    #endregion

    #region VIRTUAL

    public virtual void OnPoweredOn() {
        if(_powerableAnimator != null) {
            _powerableAnimator.SetBool(ANIM_PARAM_POWERED, true);
        }
    }

    public virtual void OnPoweredOff() {
        if (_powerableAnimator != null) {
            _powerableAnimator.SetBool(ANIM_PARAM_POWERED, false);
        }
    }

    #endregion


    public void AddPowerSource(PowerSource source) {
        if (!_currentPowerSources.Contains(source)) {
            _currentPowerSources.Add(source);
            CheckPowered();
        }
    }

    public void RemovePowerSource(PowerSource source) {
        if (_currentPowerSources.Contains(source)) {
            _currentPowerSources.Remove(source);
            CheckPowered();
        }
    }


    private void CheckPowered() {
        switch(CurrentState) {
            case PowerState.Powered:
                if (_currentPowerSources.Count < _amountPowerSourceNeeded) {
                    CurrentState = PowerState.Unpowered;
                }
                break;
            case PowerState.Unpowered:
                if (_currentPowerSources.Count >= _amountPowerSourceNeeded) {
                    CurrentState = PowerState.Powered;
                }
                break;
        }
    }

  
}
