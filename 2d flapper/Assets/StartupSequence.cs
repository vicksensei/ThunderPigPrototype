using UnityEngine;
using SOEvents;
public class StartupSequence : MonoBehaviour
{
    [SerializeField]
    VoidEvent AppStart;
    // Start is called before the first frame update
    void Start()
    {
        AppStart.Raise();
        Debug.Log("Event: App Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
