
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    [SerializeField]
    SOEvents.VoidEvent RestartEvent;
    public void Restart()
    {
        RestartEvent.Raise();
    }

}
