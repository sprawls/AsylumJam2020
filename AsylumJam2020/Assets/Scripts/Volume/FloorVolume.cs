using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorVolume : MonoBehaviour
{
    [SerializeField] private int _floorID;

    void OnTriggerEnter(Collider coll) {
        if(coll.tag == "Player") {
            FloorManager.SetFloor(_floorID);
        }
    }


}
