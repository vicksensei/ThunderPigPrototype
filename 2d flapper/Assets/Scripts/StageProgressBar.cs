using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageProgressBar : MonoBehaviour
{
    [SerializeField]
    Text Text;
    [SerializeField]
    Slider Bar;
    [SerializeField]
    IntValue BossCounter;

    int count = 0;
    private void Start()
    {
        //Bar = GetComponentInChidre  <Slider>();
        Reset();
        StartCoroutine(doUpdate());

    }

    public void updateBar()
    {
        //Debug.Log("Updating score count: " + count.ToString() + " >> " + (count + 1).ToString());
        count++;
        StartCoroutine(doUpdate());
    }
    public void Reset()
    {
        ShowBar();
        count = 0;
    }
    void ClearBar()
    {
        foreach (Transform c in transform)
        {
            c.gameObject.SetActive(false);
        }
    }
    void ShowBar()
    {
        foreach (Transform c in transform)
        {
            c.gameObject.SetActive(true);
        }
    }
    IEnumerator doUpdate()
    {
        yield return new WaitForSeconds(.1f);

        Bar.maxValue = BossCounter.Value;
        Bar.value = count;
        Text.text = Bar.value.ToString() + "/" + Bar.maxValue.ToString();
        if (count >= 0 && count < BossCounter.Value)
        {
            ShowBar();
        }
        else if (count >= BossCounter.Value)
        {
            ClearBar();

        }
    }
}
