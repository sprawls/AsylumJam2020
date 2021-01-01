using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayableDirector _introDirector;

    [SerializeField] private List<GameObject> _introTexts;

    private bool _isIntroPlaying = false;
    private bool _isIntroStarted = false;
    private static bool _isMenuActive = true;
    private bool _isShowingIntroTexts = false;
    private int _introTextIndex = -1;

    public static bool IsMenuActive { get { return _isMenuActive; } }
    
    void Start()
    {
        _menu.SetActive(true);
        _player.SetActive(false);
        _introDirector.Stop();
    }


    void Update()
    {
        if(_isMenuActive) {
            if (!_isIntroStarted) {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)) {
                    StartIntro();
                }
            } else if (!_isShowingIntroTexts) {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    EndIntroEarly();
                }
            } else if (_isShowingIntroTexts) {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)) {
                    ShowNextIntroText();
                }
            }
        }
       
    }

    private void StartIntro() {
        _isIntroStarted = true;
        _isIntroPlaying = true;
        _introDirector.Play();
        _isShowingIntroTexts = false;


        _introDirector.stopped += Callback_OnIntroStopped;
    }

    private void EndIntroEarly() {
        _introDirector.Stop();
    }

    private void Callback_OnIntroStopped(PlayableDirector director) {
        StartShowingIntroText();
    }

    private void StartShowingIntroText() {
        _isShowingIntroTexts = true;
        _isIntroPlaying = false;
        ShowNextIntroText();
    }
    private void ShowNextIntroText() {
        if(_introTextIndex >= 0 && _introTextIndex < _introTexts.Count) {
            _introTexts[_introTextIndex].SetActive(false);
            Debug.Log("Hiding Intro text of index : " + _introTextIndex);
        }

        ++_introTextIndex;

        if(_introTextIndex >= 0 && _introTextIndex < _introTexts.Count) {
            _introTexts[_introTextIndex].SetActive(true);
            Debug.Log("Showing Intro text of index : " + _introTextIndex);
        } else {
            Debug.Log("Starting game with index : " + _introTextIndex);
            StartGame();
        }
    }

    private void StartGame() {
        _isMenuActive = false;
        _isIntroPlaying = false;
        _isShowingIntroTexts = false;
        _player.SetActive(true);
        _menu.SetActive(false);

    }
}
