
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    [Header("State")]
    [SerializeField]
    GameState gameState;

    GameState.State lastGameState;

    // Start is called before the first frame update
    void Awake()
    {
        gameState.state = GameState.State.Waiting;
        Debug.Log("Gamestate: Waiting");
        Time.timeScale = 1;
    }

    public void BeginGame()
    {
        gameState.state = GameState.State.Playing;
        Debug.Log("Gamestate: " + gameState.state);
        Debug.Log("Event: StartGame");
    }

    public void Pause()
    {
        lastGameState = gameState.state;
        gameState.state = GameState.State.Paused;
        Debug.Log("Gamestate: " + gameState.state);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        gameState.state = lastGameState;
        Debug.Log("Gamestate: "+ gameState.state);
        Time.timeScale = 1;
    }

    public void End()
    {
        gameState.state = GameState.State.End;
        Debug.Log("Gamestate: " + gameState.state);
    }

    public void BossFight()
    {
        gameState.state = GameState.State.BossFighting;
        Debug.Log("Gamestate: " + gameState.state);
    }

}
