using UnityEngine;
using UnityEngine.UI;

public class XPLevelLabelUI : MonoBehaviour
{
    Text Label;
    [SerializeField]
    IntValue level;

    private void Start()
    {
        Label = GetComponent<Text>();
        updateLevel();
    }

    public void updateLevel()
    {
        Label.text = "Lvl"+ level.Value;
    }
}
