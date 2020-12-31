using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRaycaster : MonoBehaviour
{

    [SerializeField] private LayerMask _interactionLayerMask = default;
    [SerializeField] private float _interactionDistance = 5;

    [Header("Fullscreen Interaction")]
    [SerializeField] private Transform _parent = default;


    private RaycastHit[] _hits;
    private InteractionBase _currentInteraction = null;
    private static FullscreenInteraction _currentFullscreenInteraction = null;

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
    public static FullscreenInteraction CurrentFullscreenInteraction { 
        get => _currentFullscreenInteraction;
        private set { _currentFullscreenInteraction = value; Debug.Log("New Fullscreen inte : " + _currentFullscreenInteraction); }
    }

    //Static
    public static bool IsInFullscreenInteraction
    {
        get { return CurrentFullscreenInteraction != null; }
    }


    #endregion

    #region LIFECYCLE

    private void OnEnable() {
        FullscreenInteraction.OnFullscreenInteractionTriggered += Callback_StartFullscreenInte;
    }

    private void OnDisable() {
        FullscreenInteraction.OnFullscreenInteractionTriggered -= Callback_StartFullscreenInte;
    }

    private void Awake() {
        _hits = new RaycastHit[4];
    }

    private void Update() {
        FindInteraction();
        HandleInputs();
    }

    #endregion

    #region FullscreenInte

    private void Callback_StartFullscreenInte(FullscreenInteraction inte) {
        if(_currentFullscreenInteraction == null) {
            _currentFullscreenInteraction = inte;
            _currentFullscreenInteraction.GoToNewParent(_parent);
        } else {
            Debug.LogWarning("Trying to start fullscreen interaction but another one is already active !", inte.gameObject);
        }
    }

    private void StopFullScreenInte() {
        if (_currentFullscreenInteraction != null) {
            _currentFullscreenInteraction.RevertToOldParent();
            _currentFullscreenInteraction = null;
        } else {
            Debug.LogWarning("Trying to remove fullscreen intereaction but none are active !");
        }
    }

    #endregion

    #region Logic

    private void FindInteraction() {
        Ray ray = new Ray(transform.position, transform.forward);
        int amtHits = Physics.RaycastNonAlloc(ray, _hits, _interactionDistance, _interactionLayerMask, QueryTriggerInteraction.Collide);
        if (amtHits > 0) {
            InteractionBase inte = _hits[0].transform.gameObject.GetComponent<InteractionBase>();
            if (inte != null) {
                CurrentInteraction = inte.GetInteraction();
            }
        } else {
            CurrentInteraction = null;
        }
    }

    private void HandleInputs() {
        if(Input.GetMouseButtonDown(0)) {
            if(IsInFullscreenInteraction) {
                StopFullScreenInte();
            } 
            else if(CurrentInteraction != null) {
                Debug.Log("Interacting with : " + CurrentInteraction);
                CurrentInteraction.Callback_OnInteracted();
            }
        }
    }

    #endregion


}
