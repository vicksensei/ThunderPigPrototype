using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMove : MonoBehaviour
{

    [Header("Parameters")]
    [SerializeField]
    float RotateSpeed;

    [SerializeField]
    float RotateRadius;
    [SerializeField]
    bool clockwise;
    [SerializeField]
    Transform OrbitCenter;
    [SerializeField]
    bool followOrbiter;

    Vector3 center;
    float angle;

    [Header("State")]
    [SerializeField]
    GameState gameState;

    public Vector3 Center { get => center; set => center = value; }

    private void Start()
    {
        if (OrbitCenter == null) { OrbitCenter = transform.parent.transform; }
        center = OrbitCenter.position;
    }

    void FixedUpdate()
    {
        if (gameState.state == GameState.State.Playing || gameState.state == GameState.State.BossFighting)
        {
            Spin(RotateRadius);
        }
    }

    void Spin(float radius)
    {

        if (followOrbiter)
            center = OrbitCenter.position;
        Vector3 offset;
        angle += RotateSpeed * Time.deltaTime;
        if (clockwise)
        {
            offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        }
        else
        {
            offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }

        transform.position = center + offset;
    }

}
