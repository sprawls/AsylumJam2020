using System;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {
    public static event Action<int> OnFloorChanged;
    private static int _currentFloor = -1;
    private static FloorManager _instance;

    [SerializeField] private List<GameObject> _floorObjects;
    [SerializeField] private List<FloorData> _floorDatas;

    private void Awake() {
        Instance = this;
        DeactivateAllFloors();
    }

    private void DeactivateFloor(int floorID) {
        if(floorID >= 0 && floorID < _floorObjects.Count) {
            GameObject floorObject = _floorObjects[floorID];
            if(floorObject != null) {
                floorObject.SetActive(false);
            }
        }
    }

    private void SetupFloor(int floorID) {
        if (floorID >= 0 && floorID < _floorObjects.Count) {
            GameObject floorObject = _floorObjects[floorID];
            if (floorObject != null) {
                floorObject.SetActive(true);
            }
        }
        if (floorID >= 0 && floorID < _floorDatas.Count) {
            FloorData floorData = _floorDatas[floorID];
            if (floorData != null) {
                RenderSettings.skybox = floorData.Skybox;
            }
        }
    }

    private void DeactivateAllFloors() {
        foreach(GameObject go in _floorObjects) {
            go.SetActive(false);
        }   
    }

    #region ACCESSORS

    public static int CurrentFloor { 
        get => _currentFloor;
        private set {

            if(_currentFloor != value) {
                Instance.DeactivateFloor(_currentFloor);
                _currentFloor = value;
                Instance.SetupFloor(_currentFloor);

                if (OnFloorChanged != null) OnFloorChanged.Invoke(_currentFloor);
                Debug.Log("Floor Changed to : " + _currentFloor);
            }

        }
    }

    public static bool IsInStaircase
    {
        get { return CableVisioner.HasBlockers; }
    }

    public static FloorManager Instance { get => _instance; private set => _instance = value; }

    public static void SetFloor(int newFloorId) {
        CurrentFloor = newFloorId;
    }

    #endregion

}
