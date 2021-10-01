using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Database", menuName = "SOValues/SkillDB")]
public class SkillDb : ScriptableObject
{
    public List<Skill> skillsList;
    public List<SkillIcon> skillsIconsList;

    public Dictionary<string, SkillIcon> skillsIconsDict()
    {
        Dictionary<string, SkillIcon> toReturn = new Dictionary<string, SkillIcon>();

        for (int i = 0; i < skillsList.Count; i++)
        {
        toReturn.Add(skillsList[i].skillName, skillsIconsList[i]);

        }

        return toReturn;
    }
}

[System.Serializable]
public class Skill
{
    public string skillName;
    public bool isLocked = true;
    public int points;
    public int maxPoints;
    public string description;
    public SkillPreReq[] skillPreReqs;

    public Skill(Skill s)
    {
        skillName = s.skillName;
        isLocked = s.isLocked;

        points = s.points;
        maxPoints =s.maxPoints;
        description = s.description;
        skillPreReqs = new SkillPreReq[s.skillPreReqs.Length];
        for (int i = 0; i < s.skillPreReqs.Length; i++)
        {
            skillPreReqs[i] = new SkillPreReq(s.skillPreReqs[i]);
        }

}

}
[System.Serializable] 
public class SkillPreReq
{
    public string SkillName;
    public int requiredPoints;

    public SkillPreReq(string sName, int rPoints)
    {
        SkillName = sName;
        requiredPoints = rPoints;
    }
    public SkillPreReq(SkillPreReq req)
    {
        SkillName = req.SkillName;
        requiredPoints = req.requiredPoints;
    }
}
[System.Serializable]
public class SkillIcon
{
    public string skillName;
    public Sprite skillIcon;
}