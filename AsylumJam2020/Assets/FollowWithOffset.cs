using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowWithOffset : MonoBehaviour
{
    public Transform _target;
    public Vector3 _offset = new Vector3(0, 0, -1);

    void Update()
    {
        if(_target != null) {
            transform.position = _target.position + _offset;
        }
    }
}
