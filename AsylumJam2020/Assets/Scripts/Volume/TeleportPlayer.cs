using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private int _timesItCanTrigger = -1;

    private int _timesTrigger = 0;

    void OnTriggerEnter(Collider coll) {
        if(coll.tag == "Player" && (_timesItCanTrigger<0 || _timesTrigger< _timesItCanTrigger)) {
            coll.transform.position += _offset;
            ++_timesTrigger;
        }
    }


}
