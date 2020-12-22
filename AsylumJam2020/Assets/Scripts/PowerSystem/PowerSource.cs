using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour
{

    [SerializeField] private List<Powerable> _powerables;
    [SerializeField] private bool _powered = false;

    #region ACCESSORS

    public bool Powered { 
        get => _powered;
        set { 
            _powered = value; 
            UpdatePower(); 
        }
    }

    #endregion

    private void Start() {
        UpdatePower();
    }

    private void UpdatePower() {
        foreach(Powerable powerable in _powerables) {
            if(powerable != null) {
                if (_powered) {
                    powerable.AddPowerSource(this);
                } else {
                    powerable.RemovePowerSource(this);
                }
            } else {
                Debug.LogError("Powerable is null in power source !", gameObject);
            }

        }
    }

    public void TogglePower() {
        Powered = !Powered;
    }

}
