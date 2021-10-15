using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintString : MonoBehaviour
{
    public string message;
    public void OnEvent()
    {
        Debug.Log(message);
    }
}
