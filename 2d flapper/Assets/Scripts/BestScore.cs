using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BestScore : MonoBehaviour
{
    [SerializeField]
    ScoreManager SM;
    [SerializeField]
    GameObject HS;
    [SerializeField]
    GameObject ScoreFrame;

    [SerializeField]
    TMP_Text Kills;
    [SerializeField]
    TMP_Text Combo;
    [SerializeField]
    TMP_Text Accuracy;
    [SerializeField]
    TMP_Text PerfectionBonus;

    // Start is called before the first frame update
    void OnEnable()
    {
    }

    public void ShowGO()
    {
        StartCoroutine(TextAnim());
    }

    public void CheckHighScore()
    {
        if (SM.newHigh) { HS.SetActive(true); }
        else { HS.SetActive(false); }
    }

    private IEnumerator TextAnim()
    {
        Debug.Log("starting TextAnim");
        ProgressionObject p = SM.CurSavefile;
        int kills = p.CurRunKills;
        int combo = p.CurRunMaxCombo;
        int bonus = SM.getComboBonus();
        bool perfection = p.IsCurrentRunPerfect;
        bool noMiss = p.IsCurrentRunAccurate;

        Debug.Log("Kills: " + kills + " combo: " + combo + " Perfect?:" + perfection + " no Miss?:" + noMiss + " bonus: " + bonus + " .. Total score: " + p.CurRunScoreWithBonus);
        Kills.text = "Kills..." + kills;
        Combo.text = "Max Combo..." + combo + "... Score + " + bonus + "!";
        Accuracy.text = "100% Accuracy Bonus...x2!";
        PerfectionBonus.text = "Perfection Bonus... x2!";
        Kills.gameObject.SetActive(true);
        yield return CoroutineAnims.growShrink(Kills.transform, 2);
        Debug.Log("Kills done, checking combo");
        if (combo >= 10)
        {
            Combo.gameObject.SetActive(true);
            yield return CoroutineAnims.growShrink(Combo.transform, 2);
        }

        if (combo >= 10 && noMiss)
        {
            Accuracy.gameObject.SetActive(true);
            yield return CoroutineAnims.growShrink(Accuracy.transform, 2);
        }

        if (perfection && kills > 1)
        {
            PerfectionBonus.gameObject.SetActive(true);
            yield return CoroutineAnims.growShrink(PerfectionBonus.transform, 2);
        }

        ScoreFrame.gameObject.SetActive(true);

        CheckHighScore();
    }

}
