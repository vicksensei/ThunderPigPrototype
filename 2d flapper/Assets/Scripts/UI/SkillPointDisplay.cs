using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SOEvents;

public class SkillPointDisplay : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    VoidEvent Appeared;
    [Header("Components")]
    [SerializeField]
    TMP_Text SkillPoints;
    [Header("Progression")]
    [SerializeField]
    ProgressionObject currentProgression;

    private void Start()
    {
        RefreshSkillPointDisplay();
    }
    private void OnEnable()
    {
        Appeared.Raise();
    }
    public void RefreshSkillPointDisplay()
    {
        SkillPoints.text = currentProgression.Skillpoints.ToString();
    }

}
