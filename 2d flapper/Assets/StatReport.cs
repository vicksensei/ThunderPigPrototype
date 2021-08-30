using UnityEngine;
using UnityEngine.UI;

public class StatReport : MonoBehaviour
{

    [SerializeField]
    Text HiScore;
    [SerializeField]
    Text Stages;
    [SerializeField]
    Text Level;
    [SerializeField]
    Text Charge;
    [SerializeField]
    Text MHP;

    [SerializeField]
    ProgressionObject saveFile;

    private void Awake()
    {
        RefreshText();
    }
    public void RefreshText()
    {
        HiScore.text = saveFile.HighScore.ToString();
        Stages.text = saveFile.FurthestDifficulty.ToString();
        Level.text = saveFile.Level.ToString();
        Charge.text = saveFile.MaxCharge.ToString();
        MHP.text = saveFile.MaxHP.ToString();
    }
}
