using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpButton : MonoBehaviour
{

    [SerializeField]
    ProgressionObject saveFile;
    
    public void Show()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(saveFile.Skillpoints > 0);
        }
    }
    public void Hide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
