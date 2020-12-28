using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableVisionDeactivationVolume : MonoBehaviour
{

    void OnTriggerEnter(Collider coll) {
        if(coll.tag == "Player") {
            CableVisioner.AddBlocker(gameObject);
        }
    }

    void OnTriggerExit(Collider coll) {
        if (coll.tag == "Player") {
            CableVisioner.RemoveBlocker(gameObject);
        }
    }


}
