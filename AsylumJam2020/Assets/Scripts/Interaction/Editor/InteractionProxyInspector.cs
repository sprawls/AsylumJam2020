using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InteractionProxy))]
public class InteractionProxyInspector : Editor
{

    SerializedProperty _linkedInteraction;

    void OnEnable() {
        _linkedInteraction = serializedObject.FindProperty("_linkedInteraction");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_linkedInteraction);
        serializedObject.ApplyModifiedProperties();
    }

}
