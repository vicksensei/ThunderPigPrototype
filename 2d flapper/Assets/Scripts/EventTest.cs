using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    public void VoidEventRaised()
    {
        Debug.Log("Void Event!");
    }
    public void StringEventRaised(string s)
    {
        Debug.Log("String Event! ->" +s);
    }
}
