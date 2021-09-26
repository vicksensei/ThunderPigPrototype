using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacktoMenu : MonoBehaviour
{
    [SerializeField]
    SOEvents.VoidEvent MenuEvent;

    public void BackToMenu()
    {
        MenuEvent.Raise();
        Debug.Log("Event:Menu");
    }
}
