using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeManager : MonoBehaviour
{

    [Header("Value Objects")]
    [SerializeField]
    IntValue CurrentCharge;
    [SerializeField]
    IntValue MaxCharge;
    [SerializeField]
    IntValue CurrentFlaps;
    [SerializeField]
    IntValue FlapsToCharge;


    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent ChargeChangedEvent;


    [Header("State")]
    [SerializeField]
    GameState gameState;

    [Header("Prefabs")]
    [SerializeField]
    GameObject EnergyPotionEffect;

    private void Awake()
    {
        CurrentCharge.Value = MaxCharge.Value;
        CurrentFlaps.Value = 0;
        ChargeChangedEvent.Raise();
    }

    public void OnFlap()
    {
        CurrentFlaps.Value++;
        if (CurrentFlaps.Value == FlapsToCharge.Value)
        {
            if(CurrentCharge.Value< MaxCharge.Value)
            {
                GainEnergy();
            }
            else
            {
                CurrentFlaps.Value = FlapsToCharge.Value - 1;
            }
        }
    }

    public void GainEnergy()
    {
        CurrentCharge.Value++;
        CurrentFlaps.Value = 0;
        ChargeChangedEvent.Raise();
        //Instantiate(EnergyPotionEffect, transform);
    }

    public void UseEnergyPot()
    {
        if (CurrentCharge.Value < MaxCharge.Value)
        {
            GainEnergy();
        }
    }
    public void FillEnergy()
    {
        if (CurrentCharge.Value != MaxCharge.Value)
        {
            CurrentCharge.Value = MaxCharge.Value;
            Instantiate(EnergyPotionEffect, transform);
            ChargeChangedEvent.Raise();
        }
    }

    public void OnShoot()
    {
        if (CurrentCharge.Value > 0)
        {
            CurrentCharge.Value--;
            ChargeChangedEvent.Raise();
        }
    }

}
