using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [Header("UI field")]
    [SerializeField]
    Text scoreDisplay;

    [Header("Value Objects")]
    [SerializeField]
    ProgressionObject saveFile;
    [SerializeField]
    bool isHighScore = false;
    [SerializeField]
    bool isEndingScore = false;
    private void Start()
    {
        UpdateScore();
    }

    private void OnEnable()
    {
        UpdateScore();
    }

    public void Init()
    {
        scoreDisplay.text = "";
    }
    public void UpdateScore()
    {
        string toShow;
        if (isHighScore) { toShow = saveFile.HighScore.ToString(); }
        else
        {
            if (isEndingScore)
            {
                toShow = saveFile.CurRunScoreWithBonus.ToString();
            }
            else
            {
                toShow = saveFile.CurRunKills.ToString();

            }
        }
        scoreDisplay.text = toShow;
    }
}
