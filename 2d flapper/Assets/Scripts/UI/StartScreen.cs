using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{

    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent StartGameEvent;

    public void StartGame()
    {
        StartGameEvent.Raise();
    }

}
