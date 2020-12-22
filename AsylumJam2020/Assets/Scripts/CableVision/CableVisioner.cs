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

    private bool _cableVisionActive = false;

    public bool CableVisionActive { 
        get => _cableVisionActive;
        set { 
            _cableVisionActive = value;
            _cableVisionAnimator.SetBool(ANIM_PARAM_VISIONACTIVE, value);
        }
    }

    private void Update() {
        CableVisionActive = (Input.GetMouseButton(1));
    }

}
