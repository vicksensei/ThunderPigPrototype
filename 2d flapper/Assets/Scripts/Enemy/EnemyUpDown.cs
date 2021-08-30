using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpDown : MonoBehaviour
{

    [Header("Values")]
    [SerializeField]
    float speed;
    [SerializeField]
    float height;
    [SerializeField]
    bool upDir;
    int multiplier;
    [SerializeField]
    ProgressionObject SaveFile;

    [Header("State")]
    [SerializeField]
    GameState gameState;


    Vector3 startPos;

    public float Height { get => height; set => height = value; }
    public Vector3 StartPos { get => startPos; set => startPos = value; }

    // Start is called before the first frame update
    void Awake()
    {
        StartPos = transform.position;
        multiplier = -1;
        if (upDir) multiplier = 1;
        if (SaveFile.CurrentDifficulty > 1)
        {
            speed += SaveFile.CurrentDifficulty * .2f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameState.state == GameState.State.Playing || gameState.state == GameState.State.BossFighting)
        {
            float y = StartPos.y + (Mathf.PingPong(speed * Time.time, Height) * multiplier);
            Vector3 pos = new Vector3(transform.position.x, y, transform.position.z);
            transform.position = pos;
        }
    }
}
