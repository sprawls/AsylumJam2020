using System;
using DG.Tweening;
using UnityEngine;

public class FullscreenInteraction : InteractionBase
{
    public static event Action<FullscreenInteraction> OnFullscreenInteractionTriggered;

    [SerializeField] private Transform _noteParent;
    [SerializeField] private GameObject _notePrefab;

    private GameObject _noteInstance;
    private Sequence _sequence = null;
    private Vector3     _ogPosition;
    private Quaternion  _ogRotation;
    private Vector3     _ogScale;

    private float _animTime = 0.4f;

    public BasicAudioEventPlayer basicAudioEventPlayer;

    protected override void Awake() {
        _noteInstance = GameObject.Instantiate(_notePrefab, _noteParent, false);
        _ogPosition = _noteInstance.transform.localPosition;
        _ogRotation = _noteInstance.transform.localRotation;
        _ogScale = _noteInstance.transform.localScale;

        _noteInstance.SetActive(false);
        base.Awake();

        basicAudioEventPlayer = GetComponentInChildren<BasicAudioEventPlayer>();
    }

    public override void Callback_OnInteracted() {
        if (Powered) {
            if(OnFullscreenInteractionTriggered != null) OnFullscreenInteractionTriggered.Invoke(this);
        }

        base.Callback_OnInteracted();
    }

    private void StopPreviousSequence() {
        if(_sequence != null) {
            _sequence.Kill(true);
        }
    }

    public void GoToNewParent(Transform newParent) {
        StopPreviousSequence();

        if (basicAudioEventPlayer != null)
        {
            basicAudioEventPlayer.PlayInteractionOnEvent.Invoke();
        }

        _noteInstance.SetActive(true);
        _noteInstance.transform.parent = newParent;
        _sequence = DOTween.Sequence();
        _sequence.Insert(0, _noteInstance.transform.DOLocalMove(Vector3.zero, _animTime));
        _sequence.Insert(0, _noteInstance.transform.DOLocalRotate(Vector3.zero, _animTime));
        _sequence.Insert(0, _noteInstance.transform.DOScale(Vector3.one, _animTime));
    }

    public void RevertToOldParent() {
        StopPreviousSequence();

        if (basicAudioEventPlayer != null)
        {
            basicAudioEventPlayer.PlayInteractionOffEvent.Invoke();
        }

        _noteInstance.transform.parent = _noteParent;
        _sequence = DOTween.Sequence();
        _sequence.Insert(0, _noteInstance.transform.DOLocalMove(_ogPosition, _animTime));
        _sequence.Insert(0, _noteInstance.transform.DOLocalRotate(_ogRotation.eulerAngles, _animTime));
        _sequence.Insert(0, _noteInstance.transform.DOScale(_ogScale, _animTime));
        _sequence.onComplete += Callback_OnNoteReverted;
    }

    private void Callback_OnNoteReverted() {
        _noteInstance.gameObject.SetActive(false);
    }
}
