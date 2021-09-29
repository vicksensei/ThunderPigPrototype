
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    [Header("Value Objects")]
    [SerializeField]
    ProgressionObject SaveFile;


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
        SaveFile.CurHP--;
        HealthChangedEvent.Raise();
        if (SaveFile.CurHP == 0)
        {
            PlayerDeadEvent.Raise();
            Time.timeScale = 0;
        }
    }
    public void IncreaseHealth()
    {
        if (SaveFile.CurHP < SaveFile.MaxHP)
        {
            SaveFile.CurHP++;
            HealthChangedEvent.Raise();
            Instantiate(PotionEffect, transform);
        }
    }

    public void ResetHealth()
    {
        if (SaveFile.CurrentDifficulty == 1)
        {
            SaveFile.CurHP = SaveFile.MaxHP;
        }
        HealthChangedEvent.Raise();
    }
}
