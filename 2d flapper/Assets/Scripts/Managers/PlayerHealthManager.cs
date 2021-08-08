
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    [Header("Value Objects")]
    [SerializeField]
    IntValue Health;


    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent HealthChangedEvent;
    [SerializeField]
    SOEvents.VoidEvent PlayerDeadEvent;
    [SerializeField]
    SOEvents.VoidEvent StopGameEvent;


    [Header("State")]
    [SerializeField]
    GameState gameState;

    public void ReduceHealth()
    {
        Health.Vlaue--;
        HealthChangedEvent.Raise();
        if (Health.Vlaue == 0)
        {
            StopGameEvent.Raise();
            PlayerDeadEvent.Raise();
        }
    }
    public void IncreaseHealth()
    {
        Health.Vlaue++;
        HealthChangedEvent.Raise();
    }

    public void ResetHealth()
    {
        Health.Vlaue = 3;
        HealthChangedEvent.Raise();
    }
}
