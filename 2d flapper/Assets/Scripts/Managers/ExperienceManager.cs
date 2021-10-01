using UnityEngine;
using SOEvents;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField]
    ProgressionObject PO;
    [SerializeField]
    VoidEvent LevelUp;
    int progrssionMult = 10;

    public void earnEXP(int howMuch)
    {
        int curLevel = PO.Level;
        PO.ExperiencePoints += howMuch;
        if (PO.ExperiencePoints >= curLevel * progrssionMult)
        {
            PO.Level++;
            PO.ExperiencePoints -= curLevel * progrssionMult;
            LevelUp.Raise();
            PO.CurHP = PO.MaxHP;
            PO.CurCharge = PO.MaxCharge;
            PO.Skillpoints++;
        }
    }
}
