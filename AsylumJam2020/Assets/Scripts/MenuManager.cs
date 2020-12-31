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

    private bool _isIntroPlaying = false;
    private static bool _isMenuActive = true;

    public static bool IsMenuActive { get { return _isMenuActive; } }
    
    void Start()
    {
        _menu.SetActive(true);
        _player.SetActive(false);
        _introDirector.Stop();
    }


    void Update()
    {
        if(_isMenuActive && !_isIntroPlaying) {
            if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)) {
                StartIntro();
            }
        } else if(_isMenuActive) {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                EndIntroEarly();
            }
        }
    }

    private void StartIntro() {
        _isIntroPlaying = true;
        _introDirector.Play();

        _introDirector.stopped += Callback_OnIntroStopped;
    }

    private void EndIntroEarly() {
        _introDirector.Stop();
    }

    private void Callback_OnIntroStopped(PlayableDirector director) {
        _isMenuActive = false;
        _isIntroPlaying = false;
        _player.SetActive(true);
        _menu.SetActive(false);

    }
}
