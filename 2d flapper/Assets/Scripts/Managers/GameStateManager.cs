
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    [Header("State")]
    [SerializeField]
    GameState gameState;
    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent StartGame;
    [SerializeField]
    SOEvents.VoidEvent StopGame;

    GameState.State lastGameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState.state = GameState.State.Waiting;
        Debug.Log("Gamestate: Waiting");
    }

    public void BeginGame()
    {
        gameState.state = GameState.State.Playing;
        Debug.Log("Gamestate: Playing");
        Debug.Log("Event: StartGame");
    }

    public void Pause()
    {
        gameState.state = GameState.State.Paused;
        Debug.Log("Gamestate: Paused");
    }

    public void Continue()
    {
        gameState.state = GameState.State.Playing;
        Debug.Log("Gamestate: Playing");
    }

    public void End()
    {
        gameState.state = GameState.State.End;
        Debug.Log("Gamestate: End");
    }

    public void BossFight()
    {
        gameState.state = GameState.State.BossFighting;
        Debug.Log("Gamestate: Fighting Boss!");
    }

}
