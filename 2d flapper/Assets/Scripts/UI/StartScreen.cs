using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{

    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent StartGameEvent;
    [SerializeField]
    SOEvents.VoidEvent MenuEvent;
    [SerializeField]
    SOEvents.VoidEvent ControlsEvent;

    public void StartGame()
    {
        StartGameEvent.Raise();
        Debug.Log("Event:StartGame");
    }
    public void BackToMenu()
    {
        MenuEvent.Raise();
        Debug.Log("Event:Menu");
    }
    public void ControlsScreen()
    {
        ControlsEvent.Raise();
        Debug.Log("Event:Controls");
    }

}
