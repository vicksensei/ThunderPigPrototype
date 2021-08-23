using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [Header("UI field")]
    [SerializeField]
    Text scoreDisplay;

    [Header("Value Objects")]
    [SerializeField]
    IntValue scoreObject;

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
        scoreDisplay.text = scoreObject.Value.ToString();
    }
}
