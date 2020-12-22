using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    void OnTriggerEnter(Collider coll) {
        if(coll.tag == "Player") {
            coll.transform.position += _offset;
        }
    }


}
