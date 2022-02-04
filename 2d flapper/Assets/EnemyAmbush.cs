using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmbush : MonoBehaviour
{
    [Header("State")]
    [SerializeField]
    GameState gameState;
    [SerializeField]
    ProgressionObject SaveFile;

    [Header("Movement")]
    [SerializeField]
    float speed;

    [SerializeField]
    Vector3 startPos;
    [SerializeField]
    Vector3 endPos;
    [SerializeField]
    float startDelay;
    [SerializeField]
    float endDelay;

    public Vector3 StartPos { get => startPos; set => startPos = value; }
    public Vector3 EndPos { get => endPos; set => endPos = value; }

    enum moveState {
        offscreen,
        shooting,
        hiding,
        onscreen
    };

    moveState currentState;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        currentState = moveState.offscreen;
        if (SaveFile.CurrentDifficulty > 1)
        {
            speed += SaveFile.CurrentDifficulty * .2f;
        }
    }


    void FixedUpdate()
    {
        if (gameState.state == GameState.State.Playing || gameState.state == GameState.State.BossFighting)
        {
            if(currentState == moveState.offscreen)
            {
                timer += Time.fixedDeltaTime;
                if(timer >= startDelay)
                {
                    timer = 0;
                    currentState = moveState.onscreen;
                }
            }
            else if (currentState == moveState.onscreen)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos, speed *Time.fixedDeltaTime);
                if (Vector3.Distance(transform.position, endPos) < 0.001f)
                {
                    currentState = moveState.shooting;
                }
            }
            else if (currentState == moveState.shooting)
            {
                timer += Time.fixedDeltaTime;
                if (timer >= endDelay)
                {
                    timer = 0;
                    currentState = moveState.hiding;
                }
            }
            else if (currentState == moveState.hiding)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.fixedDeltaTime);
                if (Vector3.Distance(transform.position, startPos) < 0.001f)
                {
                    currentState = moveState.offscreen;
                }
            }
            Debug.Log(timer);
        }

    }
}
