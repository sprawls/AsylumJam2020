using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scrypt/FloorData", order = 1)]
public class FloorData : ScriptableObject
{
    [Header("Skybox and Sun")]
    [SerializeField] private Material _skybox;
    [SerializeField] private Vector3 _sunPosition;
    [SerializeField] private Quaternion _sunRotation;
    [SerializeField] private Color _sunColor;
    [SerializeField] private float _sunIntensity;

    [Header("FlashLight")]
    [SerializeField] private float _lightSlerpSpeed = 0.13f;
    [SerializeField] private float _lightChangeSpeed = 0.1f;
    [SerializeField] private Vector2 _lightAngleChange = new Vector2(55, 65);
    [SerializeField] private Vector2 _lightAngleGap = new Vector2(0, 1);


    public Material     Skybox { get => _skybox; }
    public Vector3      SunPosition { get => _sunPosition; set => _sunPosition = value; }
    public Quaternion   SunRotation { get => _sunRotation; set => _sunRotation = value; }
    public Color        SunColor { get => _sunColor; set => _sunColor = value; }
    public float        SunIntensity { get => _sunIntensity; set => _sunIntensity = value; }

    public float    LightSlerpSpeed { get => _lightSlerpSpeed; set => _lightSlerpSpeed = value; }
    public float    LightChangeSpeed { get => _lightChangeSpeed; set => _lightChangeSpeed = value; }
    public Vector2  LightAngleChange { get => _lightAngleChange; set => _lightAngleChange = value; }
    public Vector2  LightAngleGap { get => _lightAngleGap; set => _lightAngleGap = value; }
}
