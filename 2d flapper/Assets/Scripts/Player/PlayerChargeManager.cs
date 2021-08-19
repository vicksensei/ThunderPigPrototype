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
            if(CurrentCharge.Value<= MaxCharge.Value)
            {
                CurrentCharge.Value++;
                CurrentFlaps.Value = 0;
                ChargeChangedEvent.Raise();
            }
            else
            {
                CurrentFlaps.Value = FlapsToCharge.Value - 1;
            }
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
