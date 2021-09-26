using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateTester : MonoBehaviour
{
    public GameState GS;
    public GameState.State state;

    private void Update()
    {
        GS.state = state;
    }
}
