
using UnityEngine;

[CreateAssetMenu(fileName = "New Game State", menuName = "Game State")]
public class GameState : ScriptableObject
{
    public enum State
    {
        Waiting,
        Playing,
        Paused,
        BossFighting,
        End
    };

    public State state;
}
