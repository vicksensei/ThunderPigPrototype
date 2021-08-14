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
    }
    public void AddToScore(int amount)
    {
        score.Value += amount;
        ScoreChanged.Raise();
        if(score.Value == waspBossScore.Value)
        {
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
