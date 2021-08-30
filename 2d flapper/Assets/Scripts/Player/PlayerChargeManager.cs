using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeManager : MonoBehaviour
{

    [Header("Value Objects")]
    [SerializeField]
    IntValue CurrentFlaps;
    [SerializeField]
    ProgressionObject SaveFile;

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
        SaveFile.CurCharge = SaveFile.MaxCharge;
        CurrentFlaps.Value = 0;
        ChargeChangedEvent.Raise();
    }

    public void OnFlap()
    {
        CurrentFlaps.Value++;
        if (CurrentFlaps.Value == SaveFile.FlapsToCharge)
        {
            if(SaveFile.CurCharge < SaveFile.MaxCharge)
            {
                GainEnergy();
            }
            else
            {
                CurrentFlaps.Value = SaveFile.FlapsToCharge - 1;
            }
        }
    }

    public void GainEnergy()
    {
        SaveFile.CurCharge++;
        CurrentFlaps.Value = 0;
        ChargeChangedEvent.Raise();
    }

    public void UseEnergyPot()
    {
        if (SaveFile.CurCharge < SaveFile.MaxCharge)
        {
            GainEnergy();
        }
    }

    public void FillEnergy()
    {
        if (SaveFile.CurCharge != SaveFile.MaxCharge)
        {
            SaveFile.CurCharge = SaveFile.MaxCharge;
            Instantiate(EnergyPotionEffect, transform);
            ChargeChangedEvent.Raise();
        }
    }

    public void OnShoot()
    {
        if (SaveFile.CurCharge > 0)
        {
            SaveFile.CurCharge--;
            ChargeChangedEvent.Raise();
        }
    }

}
