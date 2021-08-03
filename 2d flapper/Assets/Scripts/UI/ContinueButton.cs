using UnityEngine;


public class ContinueButton : MonoBehaviour
{
    [SerializeField]
    SOEvents.VoidEvent ContinueEvent;

    public void Continue()
    {
        ContinueEvent.Raise();
    }
}
