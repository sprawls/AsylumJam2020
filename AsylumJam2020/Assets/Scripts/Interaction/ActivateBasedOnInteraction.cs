using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBasedOnInteraction : MonoBehaviour
{
    [SerializeField] private InteractionBase _linkedInteraction;
    [SerializeField] private GameObject _toActivate;
    [SerializeField] private GameObject _toDeactivate;

    // Start is called before the first frame update
    void OnEnable()
    {
        if(_linkedInteraction) _linkedInteraction.OnInteractionTriggered += OnInteractionTriggered;
    }

    private void OnDisable() {
        if (_linkedInteraction) _linkedInteraction.OnInteractionTriggered -= OnInteractionTriggered;
    }

    private void OnInteractionTriggered(bool active) {
        StartCoroutine(TriggerOnDelay());
    }
    private IEnumerator TriggerOnDelay() {
        yield return null;
        if(_toActivate) _toActivate.SetActive(true);
        if(_toDeactivate) _toDeactivate.SetActive(false);
    }
}
