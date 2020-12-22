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
    private bool _isStarted = false;

    private static string ANIM_PARAM_POWERED = "Powered";

    #region ACCESSORS

    public PowerState CurrentState {
        get {
            return _powerState;
        }
        private set { 
            //if(_powerState != value) {
                _powerState = value;

                if (_powerState == PowerState.Powered) OnPoweredOn();
                else OnPoweredOff();
            //}

        }
    }

    public bool Powered {
        get { return CurrentState == PowerState.Powered; }
    }

    protected Animator Animator { get => _powerableAnimator; set => _powerableAnimator = value; }

    #endregion

    #region LIFECYCLE

    protected virtual void Awake() {
        _isStarted = false;
        _currentPowerSources = new List<PowerSource>(4);
        OnPoweredOff();
    }

    protected virtual void Start() {
        CheckPowered();
        _isStarted = true;
    }

    private void OnEnable() {
        if(_isStarted) {
            CheckPowered();
        }
    }

    private void OnDisable() {
        if (_isStarted) {
            CheckPowered();
        }
    }

    #endregion

    #region VIRTUAL

    public virtual void OnPoweredOn() {
        if(Animator != null) {
            Animator.SetBool(ANIM_PARAM_POWERED, true);
        }
    }

    public virtual void OnPoweredOff() {
        if (Animator != null) {
            Animator.SetBool(ANIM_PARAM_POWERED, false);
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
        if (_currentPowerSources.Count < _amountPowerSourceNeeded) {
            CurrentState = PowerState.Unpowered;
        }
        else if (_currentPowerSources.Count >= _amountPowerSourceNeeded) {
            CurrentState = PowerState.Powered;
        }
    }

  
}
