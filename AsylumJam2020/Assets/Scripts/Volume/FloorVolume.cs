using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorVolume : MonoBehaviour
{
    [SerializeField] private int _floorID;
    [SerializeField] private bool _startsActive;

    private void Start() {
        if (_startsActive) {
            Debug.Log(string.Format("Floor {0} started Active", _floorID), gameObject);
            Activate();
        }
    }

    void OnTriggerEnter(Collider coll) {
        if(coll.tag == "Player") {
            Activate();
        }
    }

    private void Activate() {
        FloorManager.SetFloor(_floorID);
    }


}
