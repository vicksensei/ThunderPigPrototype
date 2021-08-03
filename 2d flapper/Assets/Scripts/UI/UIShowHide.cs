using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShowHide : MonoBehaviour
{
    public void Show()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
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
