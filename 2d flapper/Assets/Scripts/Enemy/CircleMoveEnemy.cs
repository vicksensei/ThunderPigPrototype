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
    [SerializeField]
    ProgressionObject saveFile;

    [Header("Parameters")]
    [SerializeField]
    bool clockwise;
    [SerializeField]
    Transform OrbitCenter;
    [SerializeField]
    bool followOrbiter;
    [SerializeField]
    bool spiralsIn;
    [SerializeField]
    float spiralSpeed;

    Vector3 center;
    [SerializeField]
    float angle;
    [SerializeField]
    float currentRadius;
    float currentSpeed;

    [Header("State")]
    [SerializeField]
    GameState gameState;

    public Vector3 Center { get => center; set => center = value; }

    private void Awake()
    {
        currentSpeed = RotateSpeed.Value;
        if(OrbitCenter == null) { OrbitCenter = transform.parent.transform; }
        center = OrbitCenter.position;
        currentRadius = 0f;
        if(saveFile.CurrentDifficulty > 1)
        {
            currentSpeed += saveFile.CurrentDifficulty * .1f;
        }
    }

    void FixedUpdate()
    {
        if (gameState.state == GameState.State.Playing || gameState.state == GameState.State.BossFighting)
        {
            if (currentRadius <= RotateRadius.Value && spiralsIn)
            {
                currentRadius += spiralSpeed;
                Spin(currentRadius);
            }
            else
            {
                Spin(RotateRadius.Value);
            }
        }
    }

    void Spin(float radius)
    {

        if (followOrbiter)
            center = OrbitCenter.position;
        Vector3 offset;
        angle += currentSpeed * Time.deltaTime;
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
