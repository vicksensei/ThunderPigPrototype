using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeMatcher : MonoBehaviour
{
    public RectTransform toImmitate;
    public RectTransform me;
    
    public void Immitate()
    {
        me.sizeDelta = toImmitate.sizeDelta;
    }

    private void Update()
    {
    }

}
