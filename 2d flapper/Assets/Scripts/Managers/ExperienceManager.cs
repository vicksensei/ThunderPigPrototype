using UnityEngine;
using SOEvents;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField]
    ProgressionObject PO;
    [SerializeField]
    VoidEvent LevelUp;
    [SerializeField]
    GameObject expEffect;

    public void earnEXP(int howMuch)
    {
        int curLevel = PO.Level;
        PO.ExperiencePoints += howMuch;
        if (PO.ExperiencePoints >= curLevel * PO.ProgrssionMult)
        {
            PO.Level++;
            Instantiate(expEffect, transform.parent);
            PO.ExperiencePoints -= curLevel * PO.ProgrssionMult;
            LevelUp.Raise();
            PO.CurHP = PO.MaxHP;
            PO.CurCharge = PO.MaxCharge;
            PO.Skillpoints++;
        }
    }
}
