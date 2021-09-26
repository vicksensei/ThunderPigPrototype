using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Value Objects")]
    [SerializeField]
    IntValue waspBossScore;
    [SerializeField]
    GameState gameState;
    [SerializeField]
    ProgressionObject curSavefile;

    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent ScoreChanged;
    [SerializeField]
    SOEvents.VoidEvent HiScoreChanged;
    [SerializeField]
    SOEvents.VoidEvent waspBossTime;

    public void ResetScore()
    {
        curSavefile.CurScore = 0;
        curSavefile.CurrentDifficulty = 1;
        ScoreChanged.Raise();

        Debug.Log("Score Reset");
    }
    public void AddToScore(int amount)
    {
        curSavefile.CurScore += amount;
        ScoreChanged.Raise();
        SetHighScore();
        if(curSavefile.CurScore >= (waspBossScore.Value * (int)curSavefile.CurrentDifficulty) && gameState.state != GameState.State.BossFighting)
        {
            Debug.Log("Goal reached: "+ (waspBossScore.Value * (int)curSavefile.CurrentDifficulty));
            waspBossTime.Raise();
        }
    }
    public void SetHighScore()
    {
        if(curSavefile.CurScore > curSavefile.HighScore)
        {
            curSavefile.HighScore = curSavefile.CurScore;
            HiScoreChanged.Raise();
        }
    }

    public void AddOne()
    {
        AddToScore(1);
    }
}
