using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayState : MonoBehaviour
{
    [SerializeField]
    GameState gameState;

    public bool play = true;
    public bool pause = false;
    public bool wait = false;
    public bool end = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (end) gameState.state = GameState.State.End;
        if (wait) gameState.state = GameState.State.Waiting;
        if (pause) gameState.state = GameState.State.Paused;
        if (play) gameState.state = GameState.State.Playing;
    }

}
