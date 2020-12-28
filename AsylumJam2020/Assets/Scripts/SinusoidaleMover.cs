using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidaleMover : MonoBehaviour
{
    public GameObject objectToMove;
    public Vector3 Offset;
    public EntityStompSoundBehaviour entityStompSoundBehaviour;
    public EntityFollowingBehaviour entityFollowingBehaviour;

    float axisX;
    float axisY;
    float axisZ;

    [Header("X Propertis")]
    [SerializeField]
    float periodX;
    [SerializeField]
    float amplitudeX;
    [SerializeField]
    float rotateX;
    [SerializeField]
    float offsetX;

    [Header("Y Propertis")]
    [SerializeField]
    float periodY;
    [SerializeField]
    float amplitudeY;
    [SerializeField]
    float rotateY;
    [SerializeField]
    float offsetY;


    [Header("Z Propertis")]
    [SerializeField]
    float periodZ;
    [SerializeField]
    float amplitudeZ;
    [SerializeField]
    float rotateZ;
    [SerializeField]
    float offsetZ;


    private void Awake()
    {
        //objectToMove = this.gameObject;
        // SetUpPos();
        GetEntitySource();
    }

    void Update()
    {
        Mouvement();
        SetEntitySource();
    }

    void GetEntitySource()
    {
        if (entityStompSoundBehaviour != null)
        {
            entityStompSoundBehaviour.SourceOffset = objectToMove.transform.position;
        }
        if (entityFollowingBehaviour != null)
        {
            entityFollowingBehaviour.SourceOffset = objectToMove.transform.position;
        }
    }
    void SetEntitySource()
    {
        if (entityStompSoundBehaviour != null)
        {
            entityStompSoundBehaviour.SourceOffset = objectToMove.transform.position;
        }
        if (entityFollowingBehaviour != null)
        {
            entityFollowingBehaviour.SourceOffset = objectToMove.transform.position;
        }
    }

    private void Mouvement()
    {
        axisX = Mathf.Sin(periodX * Time.time);
        axisY = Mathf.Sin(periodY * Time.time);
        axisZ = Mathf.Sin(periodZ * Time.time);

        objectToMove.transform.position = new Vector3((offsetX + axisX) * amplitudeX, (offsetY + axisY) * amplitudeY, (offsetZ + axisZ) * amplitudeZ);

        objectToMove.transform.localEulerAngles = new Vector3((axisX * amplitudeX) * rotateX, (axisY * amplitudeY * rotateY), (axisZ * amplitudeZ) * rotateZ);
    }
}
