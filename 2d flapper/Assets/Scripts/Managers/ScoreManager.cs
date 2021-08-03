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

    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent ScoreChanged;
    [SerializeField]
    SOEvents.VoidEvent HiScoreChanged;

    public void ResetScore()
    {
        score.Vlaue = 0;
        ScoreChanged.Raise();
    }
    public void AddToScore(int amount)
    {
        score.Vlaue += amount;
        ScoreChanged.Raise();
    }
    public void SetHighScore()
    {
        if(score.Vlaue > hiScore.Vlaue)
        {
            hiScore.Vlaue = score.Vlaue;
            HiScoreChanged.Raise();
        }
    }

    public void AddOne()
    {
        AddToScore(1);
    }
}
