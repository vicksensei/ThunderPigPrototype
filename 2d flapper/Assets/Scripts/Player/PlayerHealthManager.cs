
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    [Header("Value Objects")]
    [SerializeField]
    IntValue Health;
    [SerializeField]
    IntValue MaxHealth;


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

    [Header("Prefabs")]
    [SerializeField]
    GameObject PotionEffect;


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
        if (Health.Value < MaxHealth.Value)
        {
            Health.Value++;
            HealthChangedEvent.Raise();
            Instantiate(PotionEffect, transform);
        }
    }

    public void ResetHealth()
    {
        Health.Value = 3;
        HealthChangedEvent.Raise();
    }
}
