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
    [SerializeField]
    SOEvents.VoidEvent ComboChanged;

    public bool newHigh = false;
    int waspBossCounter = 0;
    bool perfectRun = true;
    bool noMiss = true;
    bool isDead = false;
    public ProgressionObject CurSavefile { get => curSavefile; }
    public bool PerfectRun { get => perfectRun; }
    public bool NoMiss { get => noMiss; set => noMiss = value; }

    public int getComboBonus()
    {
        int bonus;
        int combo = curSavefile.CurRunMaxCombo;
        if (combo < 10)
        {
            bonus = 0;
        }
        else
        {
            bonus = (combo / 10 * 10) * (1 + combo / 100);
        }

        return bonus;
    }
    public int getRealScore()
    {
        int score = CurSavefile.CurRunKills;
        score += getComboBonus();
        if (perfectRun)
        {
            score *= 2;
        }
        if (noMiss)
        {
            score *= 2;
        }
        return score;
    }

    public void ResetScore()
    {
        //Debug.Log("ResetScore");
        curSavefile.CurScore = 0;
        curSavefile.CurrentDifficulty = 1;
        curSavefile.CurCombo = 0;
        curSavefile.CurRunMaxCombo = 0;
        perfectRun = true;
        noMiss = true;
        curSavefile.CurRunScoreWithBonus = 0;
        curSavefile.CurRunKills = 0;
        ScoreChanged.Raise();
        ComboChanged.Raise();
        newHigh = false;
        isDead = false;

    }
    public void KillPerfection()
    {
        perfectRun = false;
    }
    public void AddToScore(int amount)
    {
        waspBossCounter += amount;
        curSavefile.CurRunKills += amount;
        curSavefile.CurScore += amount;
        curSavefile.CurRunScoreWithBonus = getRealScore();
        ScoreChanged.Raise();
        SetHighScore();
        if (waspBossCounter >= waspBossScore.Value && gameState.state != GameState.State.BossFighting)
        {
            waspBossTime.Raise();
        }
    }
    public void AddToCombo()
    {
        curSavefile.CurCombo += 1;
        if (curSavefile.CurRunMaxCombo < curSavefile.CurCombo)
        {
            curSavefile.CurRunMaxCombo = curSavefile.CurCombo;
            if (curSavefile.MaxCombo < curSavefile.CurCombo)
            {
                curSavefile.MaxCombo = curSavefile.CurCombo;
            }
        }
        ComboChanged.Raise();
    }
    public void ClearCombo()
    {
        curSavefile.CurCombo = 0;
        noMiss = false;
        Debug.Log("No  Accuracy bonus");
        ComboChanged.Raise();
    }
    public void SetHighScore()
    {
        curSavefile.CurRunScoreWithBonus = getRealScore();
        if (curSavefile.CurRunScoreWithBonus > curSavefile.HighScore)
        {
            curSavefile.CurScore = curSavefile.CurRunScoreWithBonus;
            curSavefile.HighScore = curSavefile.CurRunScoreWithBonus;
            newHigh = true;
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
    public void Died()
    {
        isDead = true;
    }
}
