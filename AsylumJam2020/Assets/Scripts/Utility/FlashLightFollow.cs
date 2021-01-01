using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightFollow : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Light _light;
    [SerializeField] private LayerMask _geometryMask;
    [SerializeField, Range(0.01f, 1f)] private float _slerpFactor;

    [Header("Light Change")]
    [SerializeField] private float _lightChangeSpeed = 1f;
    [SerializeField] private Vector2 _lightAngleChange;
    [SerializeField] private Vector2 _lightAngleGapChange;

    private RaycastHit[] _hitsCache;
    private Vector3 _targetPoint;
    private Quaternion _prevRot;

    private float _cachedLightIntensity;

    public float SlerpFactor { get => _slerpFactor; set => _slerpFactor = value; }
    public float LightChangeSpeed { get => _lightChangeSpeed; set => _lightChangeSpeed = value; }
    public Vector2 LightAngleChange { get => _lightAngleChange; set => _lightAngleChange = value; }
    public Vector2 LightAngleGapChange { get => _lightAngleGapChange; set => _lightAngleGapChange = value; }
    public Light Light { get => _light; private set => _light = value; }
    public float CachedLightIntensity { get => _cachedLightIntensity; set => _cachedLightIntensity = value; }

    private void Awake() {
        _hitsCache = new RaycastHit[4];
        _prevRot = transform.rotation;

        CachedLightIntensity = Light.intensity;
    }


    void Update()
    {
        UpdateTarget();
        LookAtTarget();
        UpdateLight();
    }

    private void UpdateLight() {
        //Intensity
        float intensity = CableVisioner.IsInCableVision ? 0f : CachedLightIntensity;
        Light.intensity = Mathf.Lerp(Light.intensity, intensity, 0.18f);

        //Spot
        float lerpValue = Mathf.PerlinNoise(Time.time * LightChangeSpeed, Time.time * LightChangeSpeed);
        float newSpotAngle = Mathf.Lerp(LightAngleChange.x, LightAngleChange.y, lerpValue);
        Light.spotAngle = newSpotAngle;

        float innerSpotAngleGap = Mathf.Lerp(LightAngleGapChange.x, LightAngleGapChange.y, lerpValue);
        Light.innerSpotAngle = newSpotAngle - innerSpotAngleGap;
    }

    private void UpdateTarget() {
        Ray ray = new Ray(_camera.position, _camera.forward);
        int amtHits = Physics.RaycastNonAlloc(ray, _hitsCache, Light.range, _geometryMask, QueryTriggerInteraction.Ignore);
        if (amtHits > 0) {
            float closest = Mathf.Infinity;
            for(int i = 0; i < amtHits; ++i) {
                Vector3 pos = _hitsCache[i].point;
                float dist = Vector3.Distance(_camera.position, pos);
                if(dist < closest) {
                    closest = dist;
                    _targetPoint = pos;
                }
            }
        } else {
            _targetPoint = _camera.position + _camera.forward * Light.range;
        }
        //Debug.DrawLine(_camera.position, _targetPoint, Color.red, 1f, false);
        //Debug.Log("New Target Position : " + _targetPoint);
    }

    private void LookAtTarget() {
        Quaternion newTargetRot = LookAt(transform.position, _targetPoint);
        transform.rotation = Quaternion.Slerp(_prevRot, newTargetRot, SlerpFactor);
        _prevRot = transform.rotation;
    }

    /// <summary>
    /// Evaluates a rotation needed to be applied to an object positioned at sourcePoint to face destPoint
    /// </summary>
    /// <param name="sourcePoint">Coordinates of source point</param>
    /// <param name="destPoint">Coordinates of destionation point</param>
    /// <returns></returns>
    public static Quaternion LookAt(Vector3 sourcePoint, Vector3 destPoint) {
        Vector3 forwardVector = Vector3.Normalize(destPoint - sourcePoint);

        float dot = Vector3.Dot(Vector3.forward, forwardVector);

        if (Mathf.Abs(dot - (-1.0f)) < Quaternion.kEpsilon) {
            return new Quaternion(Vector3.up.x, Vector3.up.y, Vector3.up.z, Mathf.PI);
        }
        if (Mathf.Abs(dot - (1.0f)) < Quaternion.kEpsilon) {
            return Quaternion.identity;
        }

        float rotAngle = (float)Mathf.Acos(dot);
        Vector3 rotAxis = Vector3.Cross(Vector3.forward, forwardVector);
        rotAxis = Vector3.Normalize(rotAxis);
        return CreateFromAxisAngle(rotAxis, rotAngle);
    }

    // just in case you need that function also
    public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle) {
        float halfAngle = angle * .5f;
        float s = (float)System.Math.Sin(halfAngle);
        Quaternion q;
        q.x = axis.x * s;
        q.y = axis.y * s;
        q.z = axis.z * s;
        q.w = (float)System.Math.Cos(halfAngle);
        return q;
    }
}
