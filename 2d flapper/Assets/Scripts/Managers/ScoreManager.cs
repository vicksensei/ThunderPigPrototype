using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Value Objects")]
    [SerializeField]
    IntValue score;
    [SerializeField]
    IntValue hiScore;
    [SerializeField]
    IntValue waspBossScore;
    [SerializeField]
    FloatValue Difficulty;
    [SerializeField]
    GameState gameState;

    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent ScoreChanged;
    [SerializeField]
    SOEvents.VoidEvent HiScoreChanged;
    [SerializeField]
    SOEvents.VoidEvent waspBossTime;

    public void ResetScore()
    {
        score.Value = 0;
        ScoreChanged.Raise();
        Difficulty.Value = 1;
        Debug.Log("Score Reset");
    }
    public void AddToScore(int amount)
    {
        score.Value += amount;
        ScoreChanged.Raise();
        SetHighScore();
        if(score.Value >= (waspBossScore.Value * (int)Difficulty.Value) && gameState.state != GameState.State.BossFighting)
        {
            Debug.Log("Goal reached: "+ (waspBossScore.Value * (int)Difficulty.Value));
            waspBossTime.Raise();
        }
    }
    public void SetHighScore()
    {
        if(score.Value > hiScore.Value)
        {
            hiScore.Value = score.Value;
            HiScoreChanged.Raise();
        }
    }

    public void AddOne()
    {
        AddToScore(1);
    }
}
