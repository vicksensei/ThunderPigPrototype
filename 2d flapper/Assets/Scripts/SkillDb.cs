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
}
[System.Serializable] 
public class SkillPreReq
{
    public string SkillName;
    public int requiredPoints;
    public int treeLevel;
}
[System.Serializable]
public class SkillIcon
{
    public string skillName;
    public Sprite skillIcon;
}