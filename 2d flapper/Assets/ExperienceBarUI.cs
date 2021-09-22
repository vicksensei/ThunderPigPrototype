using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarUI : MonoBehaviour
{
    [SerializeField]
    Text xpLabel;
    Slider xpBar;
    [SerializeField]
    IntValue level;
    [SerializeField]
    IntValue experience;
    [SerializeField]
    IntValue experienceToNext;

    private void Start()
    {
        xpBar = GetComponent<Slider>();
        updateXP();
    }

    public void updateXP()
    {
        xpBar.maxValue = level.Value * 10;
        xpBar.value = experience.Value;
        xpLabel.text = xpBar.value + " / " + xpBar.maxValue;
    }
}