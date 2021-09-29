using SOEvents;
using UnityEngine;


public class RaiseEvent : MonoBehaviour
{
    public VoidEvent EventToRaise;

    public void Raise()
    {
        EventToRaise.Raise();
    }
}
