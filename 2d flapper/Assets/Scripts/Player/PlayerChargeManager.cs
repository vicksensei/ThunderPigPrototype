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

    [Header("Sounds")]
    [SerializeField]
    AudioClip FlapSound;
    [SerializeField]
    AudioClip ChargeSound;
    private void Awake()
    {
        SaveFile.CurCharge = SaveFile.MaxCharge;
        CurrentFlaps.Value = 0;
        ChargeChangedEvent.Raise();
    }

    public void OnFlap()
    {
        CurrentFlaps.Value++;
        if (CurrentFlaps.Value == SaveFile.FlapsToCharge - SaveFile.GetSkillDict()["Charge Speed"].points)
        {
            if (SaveFile.CurCharge < SaveFile.MaxCharge + SaveFile.GetSkillDict()["Charge"].points)
            {
                GainEnergy();
            }
            else
            {
                CurrentFlaps.Value = (SaveFile.FlapsToCharge - SaveFile.GetSkillDict()["Charge Speed"].points - 1);
            }
        }
        SoundManager.instance.PlayClipRandomPitch(FlapSound, transform, 1f, 0.5f, 1f);
    }

    public void GainEnergy()
    {
        SaveFile.CurCharge++;
        CurrentFlaps.Value = 0;
        ChargeChangedEvent.Raise();
    }

    public void UseEnergyPot()
    {
        if (SaveFile.CurCharge < SaveFile.MaxCharge + SaveFile.GetSkillDict()["Charge"].points)
        {
            GainEnergy();
        }
    }

    public void FillEnergy()
    {
        int max = SaveFile.MaxCharge + SaveFile.GetSkillDict()["Charge"].points;
        if (SaveFile.CurCharge != max)
        {
            SaveFile.CurCharge += 3 + SaveFile.GetSkillDict()["Charge Power"].points;
            if (SaveFile.CurCharge > max)
            {
                SaveFile.CurCharge = max;
            }
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
