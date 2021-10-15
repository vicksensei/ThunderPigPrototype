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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
