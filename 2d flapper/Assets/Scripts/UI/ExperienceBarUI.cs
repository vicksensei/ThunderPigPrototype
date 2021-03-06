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
    ProgressionObject p;

    private void Start()
    {
        xpBar = GetComponent<Slider>();
        updateXP();
    }

    public void updateXP()
    {
        StartCoroutine(doUpdate());
    }

    IEnumerator doUpdate()
    {
        yield return new WaitForSeconds(.1f);
        xpBar.maxValue = p.Level * p.ProgrssionMult;
        xpBar.value = p.ExperiencePoints;
        xpLabel.text = xpBar.value + " / " + xpBar.maxValue;

    }
}