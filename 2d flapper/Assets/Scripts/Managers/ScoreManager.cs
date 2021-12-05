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

    int waspBossCounter = 0;

    public void ResetScore()
    {
        Debug.Log("ResetScore");
        curSavefile.CurScore = 0;
        curSavefile.CurrentDifficulty = 1;
        ScoreChanged.Raise();

    }
    public void AddToScore(int amount)
    {
        waspBossCounter += amount;
        curSavefile.CurScore += amount;
        ScoreChanged.Raise();
        SetHighScore();
        if (waspBossCounter >= waspBossScore.Value && gameState.state != GameState.State.BossFighting)
        {
            waspBossTime.Raise();
            curSavefile.CurrentDifficulty++;
        }
    }
    public void SetHighScore()
    {
        if (curSavefile.CurScore > curSavefile.HighScore)
        {
            curSavefile.HighScore = curSavefile.CurScore;
            HiScoreChanged.Raise();
        }
    }
    public void ReportScore()
    {
        ScoreChanged.Raise();

    }
    public void AddOne()
    {
        AddToScore(1);
    }

}
