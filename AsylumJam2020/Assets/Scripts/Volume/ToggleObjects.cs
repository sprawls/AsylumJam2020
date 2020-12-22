using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    [Header("Logic")]
    [SerializeField] private bool _triggersOnlyOnce = true;
    [SerializeField] private bool _triggeredOnCollision = true;
    [SerializeField] private InteractionBase _triggeredByInteraction;

    [Header("References")]
    [SerializeField] private GameObject[] _objectsToActivate;
    [SerializeField] private GameObject[] _objectsToDeactivate;

    private bool _wasTriggered = false;
    private Collider _coll;

    private bool CanTrigger {
        get { return !_triggersOnlyOnce || !_wasTriggered; }
    }

    private void Awake() {
        _wasTriggered = false;
        _coll = GetComponent<Collider>();

        if(_triggeredOnCollision && _coll != null) {
            _coll.isTrigger = true;
        }
    }

    private void OnEnable() {
        if(_triggeredByInteraction != null) {
            _triggeredByInteraction.OnInteractionTriggered += Callback_OnTriggerEnter;
        }
    }

    private void OnDisable() {
        if (_triggeredByInteraction != null) {
            _triggeredByInteraction.OnInteractionTriggered -= Callback_OnTriggerEnter;
        }
    }

    private void OnTriggerEnter(Collider coll) {
        if(_triggeredOnCollision && CanTrigger && coll.tag == "Player") {
            Trigger();
        }
    }

    private void Callback_OnTriggerEnter(bool positive) {
        if(positive && CanTrigger) {
            Trigger();
        }
    }


    private void Trigger() {
        _wasTriggered = true;

        foreach (GameObject go in _objectsToDeactivate) {
            if (go != null) {
                go.SetActive(false);
            } else {
                Debug.LogError("Invalid object in object to deactivate list", gameObject);
            }
        }
        foreach (GameObject go in _objectsToActivate) {
            if (go != null) {
                go.SetActive(true);
            } else {
                Debug.LogError("Invalid object in object to activate list", gameObject);
            }
        }
    }


}
