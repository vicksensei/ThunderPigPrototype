
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    [Header("State")]
    [SerializeField]
    GameState gameState;
    [Header("Values")]
    [SerializeField]
    ProgressionObject SaveFile;
    [SerializeField]
    ProgressionObject NewFile;
    GameState.State lastGameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState.state = GameState.State.Waiting;
        Debug.Log("Gamestate: Waiting");
        RestartTime();
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
        RestartTime();
    }

    public void End()
    {
        gameState.state = GameState.State.End;
        Debug.Log("Gamestate: " + gameState.state);
        Time.timeScale = 0;
    }

    public void BossFight()
    {
        gameState.state = GameState.State.BossFighting;
        Debug.Log("Gamestate: " + gameState.state);
        RestartTime();
    }

    public void Victory()
    {
        gameState.state = GameState.State.BossKilled;
        Debug.Log("Gamestate: " + gameState.state);
        Time.timeScale = 0;
        SaveFile.CurrentDifficulty += 1;
    }

    public void RestartTime()
    {
        Time.timeScale = 1;
    }

    public void StartNewGame()
    {
        SaveFile = Instantiate(NewFile);
    }

}
