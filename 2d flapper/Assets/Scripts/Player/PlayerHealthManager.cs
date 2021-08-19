
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
        Health.Value--;
        HealthChangedEvent.Raise();
        if (Health.Value == 0)
        {
            StopGameEvent.Raise();
            PlayerDeadEvent.Raise();
        }
    }
    public void IncreaseHealth()
    {
        Health.Value++;
        HealthChangedEvent.Raise();
    }

    public void ResetHealth()
    {
        Health.Value = 3;
        HealthChangedEvent.Raise();
    }
}
