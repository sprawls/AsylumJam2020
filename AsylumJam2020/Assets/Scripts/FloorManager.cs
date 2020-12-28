using System;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager
{
    public static event Action<int> OnFloorChanged;

    private static int _currentFloor = -1;

    public static int CurrentFloor { 
        get => _currentFloor;
        private set {

            if(_currentFloor != value) {
                _currentFloor = value;
                if (OnFloorChanged != null) OnFloorChanged.Invoke(_currentFloor);
                Debug.Log("Floor Changed to : " + _currentFloor);
            }

        }
    }


    public static void SetFloor(int newFloorId) {
        CurrentFloor = newFloorId;
    }
}
