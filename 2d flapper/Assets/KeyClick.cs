using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyClick : MonoBehaviour
{
    [SerializeField]
    SOEvents.StringEvent KeyEvent;
    [SerializeField]
    TMP_Text Letter;


    public void OnClick()
    {
        Debug.Log("Event Raised: Key Event - "+Letter.text);
        KeyEvent.Raise(Letter.text);
    }

    public void Setup(string letter)
    {
        Letter.text = letter;
    }

}
