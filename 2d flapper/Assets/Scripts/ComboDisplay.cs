using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UIElements;

public class ComboDisplay : MonoBehaviour
{

    [Header("UI field")]
    [SerializeField]
    TMP_Text comboDisplay;


    [Header("Value Objects")]
    [SerializeField]
    ProgressionObject saveFile;

    float growScale;
    int scoreMult = 0;
    int scoreBonus = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCombo();
    }

    // Update is called once per frame
    public void UpdateCombo()
    {
        if (saveFile.CurCombo < 2)
        {
            comboDisplay.text = "";
            comboDisplay.color = Color.white;
        }
        else
        {

            comboDisplay.text = saveFile.CurCombo + " Hit Combo";
            if (saveFile.CurCombo > 10)
            {
                scoreBonus = saveFile.CurRunMaxCombo / 10 * 10;
                scoreMult = saveFile.CurRunMaxCombo / 100;
                StartCoroutine(CoroutineAnims.growShrink(comboDisplay.transform, 1 + (saveFile.CurCombo / 50f)));
                comboDisplay.color = Color.red;
                comboDisplay.text += "\n +" + scoreBonus + " Score!";
            }
        }
    }



}
