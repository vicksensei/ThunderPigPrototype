using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameQuit : MonoBehaviour
{
    public void Awake()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log("platform: " + Application.platform);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("platform: " + Application.platform);
        }
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
