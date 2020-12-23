using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderSetter : MonoBehaviour
{
    [SerializeField] private Shader _shader;
    [SerializeField] private Transform _feet;
    [SerializeField] private Transform _head;
   
    private static string SH_VAR_POSITION_STRING = "HeadandPlayerPosition";
    private static int SH_VAR_POSITION_ID = -1;

    private void Awake() {
        SH_VAR_POSITION_ID = Shader.PropertyToID(SH_VAR_POSITION_STRING);
    }

    private void Update() {
        if(_shader != null) {
            Vector4 newValue = new Vector4(_head.position.x, _head.position.y, _head.position.z, _feet.position.y);
            Shader.SetGlobalVector(SH_VAR_POSITION_ID, newValue);
            Debug.Log("Set " + newValue + " in shader property : " + SH_VAR_POSITION_STRING);
        }
    }
}