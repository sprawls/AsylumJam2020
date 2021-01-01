using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScryptPlayer : MonoBehaviour
{
    [SerializeField] private List<Shader> _shaders;
    [SerializeField] private Transform _feet;
    [SerializeField] private Transform _head;
   
    private static string SH_VAR_POSITION_STRING = "HeadandPlayerPosition";
    private static int SH_VAR_POSITION_ID = -1;

    private static Vector3 _position;
    private FirstPersonAIO _fpController;

    private static bool _stoppedBecauseEndOfGame = false;

    public static Vector3 PlayerPosition {
        get { return _position;  }
    }

    public static void StopControls() {
        _stoppedBecauseEndOfGame = true;
    }

    private void Awake() {
        SH_VAR_POSITION_ID = Shader.PropertyToID(SH_VAR_POSITION_STRING);
        _fpController = GetComponent<FirstPersonAIO>();
    }

    private void Update() {
        foreach(Shader shader in _shaders) {
            if(shader != null) {
                Vector4 newValue = new Vector4(_head.position.x, _head.position.y, _head.position.z, _feet.position.y);
                Shader.SetGlobalVector(SH_VAR_POSITION_ID, newValue);
                //Debug.Log("Set " + newValue + " in shader property : " + SH_VAR_POSITION_STRING);
            }
        }

        _position = _head.position;


        //lol this is bad
        if(_fpController != null) _fpController.IsInputBlockedByGameplay = _stoppedBecauseEndOfGame || MenuManager.IsMenuActive || InteractionRaycaster.IsInFullscreenInteraction;
    }
}
