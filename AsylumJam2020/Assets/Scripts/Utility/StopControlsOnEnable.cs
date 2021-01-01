using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopControlsOnEnable : MonoBehaviour
{
    private void OnEnable() {
        ScryptPlayer.StopControls();
    }
}
