using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scrypt/FloorData", order = 1)]
public class FloorData : ScriptableObject
{
    [SerializeField] private Material _skybox;

    public Material Skybox { get => _skybox; }
}
