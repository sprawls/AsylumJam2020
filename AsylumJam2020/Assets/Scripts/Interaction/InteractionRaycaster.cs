using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRaycaster : MonoBehaviour
{

    [SerializeField] private LayerMask _interactionLayerMask = default;
    [SerializeField] private float _interactionDistance = 5;

    private RaycastHit[] _hits;
    private InteractionBase _currentInteraction = null;

    #region ACCESSORS 

    public InteractionBase CurrentInteraction { 
        get => _currentInteraction;
        private set
        {
            if(_currentInteraction != value) {

                if (_currentInteraction != null) {
                    _currentInteraction.Callback_OnHighlightedStop();
                }

                //Debug.Log(string.Format("Current Interaction changed from {0} to {1}", _currentInteraction, value));
                _currentInteraction = value;

                if (_currentInteraction != null) {
                    _currentInteraction.Callback_OnHighlightedStart();
                }
            }

        }
    }

    #endregion

    #region LIFECYCLE

    private void Awake() {
        _hits = new RaycastHit[4];
    }

    private void Update() {
        FindInteraction();
        HandleInputs();
    }

    #endregion


    #region Logic

    private void FindInteraction() {
        Ray ray = new Ray(transform.position, transform.forward);
        int amtHits = Physics.RaycastNonAlloc(ray, _hits, _interactionDistance, _interactionLayerMask, QueryTriggerInteraction.Collide);
        if (amtHits > 0) {
            InteractionBase inte = _hits[0].transform.gameObject.GetComponent<InteractionBase>();
            if (inte != null) {
                CurrentInteraction = inte;
            }
        } else {
            CurrentInteraction = null;
        }
    }

    private void HandleInputs() {
        if(Input.GetMouseButtonDown(0)) {
            if(CurrentInteraction != null) {
                CurrentInteraction.Callback_OnInteracted();
            }
        }
    }

    #endregion


}
