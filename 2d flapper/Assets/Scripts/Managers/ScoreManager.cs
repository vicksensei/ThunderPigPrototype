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
    bool isDead = false;
    public ProgressionObject CurSavefile { get => curSavefile; }

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
            bonus = (combo / 10 * 10) * (1 + (combo / 100));
        }
        //Debug.Log("Bonus:  " + bonus);
        return bonus;
    }
    public int getRealScore()
    {
        int score = CurSavefile.CurRunKills;
        score += getComboBonus();
        if (curSavefile.IsCurrentRunPerfect)
        {
            //Debug.Log("perfect run");
            score *= 2;
        }
        if (curSavefile.IsCurrentRunAccurate && curSavefile.CurCombo >= 10)
        {
            //Debug.Log("accurate run");
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
        curSavefile.IsCurrentRunPerfect = true;
        curSavefile.IsCurrentRunAccurate = true;
        curSavefile.CurRunScoreWithBonus = 0;
        curSavefile.CurRunKills = 0;
        ScoreChanged.Raise();
        ComboChanged.Raise();
        newHigh = false;
        isDead = false;

    }
    public void KillPerfection()
    {
        curSavefile.IsCurrentRunPerfect = false;
    }
    public void AddToScore(int amount)
    {
        waspBossCounter += amount;
        curSavefile.CurRunKills += amount;
        curSavefile.CurScore += amount;
        ScoreChanged.Raise();
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
        curSavefile.IsCurrentRunAccurate = false;
        //Debug.Log("No  Accuracy bonus");
        ComboChanged.Raise();
    }
    public void SetHighScore()
    {
        Debug.Log("Setting High score. This should only show up if this is true: " + isDead);
        if (isDead)
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
        SetHighScore();
    }
}
