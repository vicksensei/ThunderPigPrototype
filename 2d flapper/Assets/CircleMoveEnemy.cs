using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMoveEnemy : MonoBehaviour
{

    [Header("Value Objects")]
    [SerializeField]
    FloatValue RotateSpeed;

    [SerializeField]
    FloatValue RotateRadius;

    [Header("Parameters")]
    [SerializeField]
    bool clockwise;
    [SerializeField]
    Transform OrbitCenter;
    [SerializeField]
    bool followOrbiter;
    Vector3 center;
    float angle;

    public Vector3 Center { get => center; set => center = value; }

    private void Awake()
    {
        center = OrbitCenter.position;
    }

    void Update()
    {
        if(followOrbiter)
            center = OrbitCenter.position;
        Vector3 offset;
        angle += RotateSpeed.Value * Time.deltaTime;
        if (clockwise)
        { 
         offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * RotateRadius.Value;
        }
        else
        {
             offset = new Vector2( Mathf.Cos(angle), Mathf.Sin(angle)) * RotateRadius.Value;
        }

        transform.position = center + offset;
    }

}
