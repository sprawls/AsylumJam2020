using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableVisioner : MonoBehaviour
{
    public static int CABLE_VISION_LAYER = 8;
    private static string ANIM_PARAM_VISIONACTIVE = "Active";
    public static Color Powered_Color = Color.blue;
    public static Color Unpowered_Color = Color.red;

    [SerializeField] private Animator _cableVisionAnimator = default;
    [SerializeField] private GameObject _screenObject = default;

    private static CableVisioner _instance;
    private List<GameObject> _blockers;
    private bool _cableVisionActive = false;

    public bool CableVisionActive {
        get => _cableVisionActive;
        set {
            _cableVisionActive = value;
            _cableVisionAnimator.SetBool(ANIM_PARAM_VISIONACTIVE, value);
        }
    }
    public static bool HasBlockers { get => Instance._blockers.Count > 0; }
    public static bool IsInCableVision { get => Instance.CableVisionActive; }

    private static CableVisioner Instance { get => _instance; set => _instance = value; }

    private void Awake() {
        Instance = this;
        _blockers = new List<GameObject>(4);
    }

    private void Update() {
        CableVisionActive = (Input.GetMouseButton(1));
    }

    public static void AddBlocker(GameObject go) {
        Instance._blockers.Add(go);
        Instance._screenObject.SetActive(false);
    }

    public static void RemoveBlocker(GameObject go) {
        Instance._blockers.Remove(go);

        if(Instance._blockers.Count <= 0) {
            Instance._screenObject.SetActive(true);
        }
    }

}
