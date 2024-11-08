using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SOEvents;

public class SkillDisplay : MonoBehaviour
{
    [SerializeField]
    TMP_Text points;

    [SerializeField]
    TMP_Text skillNameText;

    [SerializeField]
    Image icon;

    [SerializeField]
    Image overlay;
    [SerializeField]
    Image border;

    [SerializeField]
    string skillName;

    [SerializeField]
    ToolTip TT;

    [SerializeField]
    ProgressionObject current;

    [SerializeField]
    public Transform childContainer;

    [SerializeField]
    StringEvent skillClicked;
    [SerializeField]
    VoidEvent updateAllSkills;
    [SerializeField]
    Skill s;

    Dictionary<string, Skill> SkillDict;
    private void Start()
    {
        //setSkill();
    }

    public void setSkill()
    {
        SkillDict = current.GetSkillDict();
        Skill skill = SkillDict[skillName];
        s = skill;
        skillNameText.text = skill.skillName;
        points.SetText(skill.points + "/" + skill.maxPoints);
        //points.text = skill.points + "/" + skill.maxPoints;
        overlay.enabled = skill.isLocked;

        string TTText;
        TTText = skill.description;
        if (skill.isLocked)
        {
            TTText += "\n [LOCKED]";
            foreach (SkillPreReq req in skill.skillPreReqs)
            {
                TTText += "\n " + req.SkillName + ": " + SkillDict[req.SkillName].points + "/" + req.requiredPoints;
                if (SkillDict[req.SkillName].points >= req.requiredPoints)
                {
                    TTText += " [DONE!]";
                }
            }
        }
        TT.text = TTText;
        icon.sprite = current.skillsDB.skillsIconsDict()[skillName].skillIcon;

    }
    public void Refresh()
    {
        Validate();
        setSkill();
    }

    void Validate()
    {
        SkillDict = current.GetSkillDict();
        int countPreReqMet = 0;
        for (int i = 0; i < SkillDict[skillName].skillPreReqs.Length; i++)
        {
            string skillToCheck = SkillDict[skillName].skillPreReqs[i].SkillName;
            int pointsReq = SkillDict[skillName].skillPreReqs[i].requiredPoints;
            if (SkillDict.ContainsKey(skillToCheck))
            {
                if (SkillDict[skillToCheck].points >= pointsReq)
                {
                    countPreReqMet++;
                }
            }
            else if (skillToCheck == "Level")
            {

                if (current.Level >= pointsReq)
                {
                    countPreReqMet++;
                }
            }
            else
            {
                Debug.Log("Incorrect Skill");
            }
        }
        if (countPreReqMet >= SkillDict[skillName].skillPreReqs.Length)
        {
            SkillDict[skillName].isLocked = false;
        }
        else
        {
            SkillDict[skillName].isLocked = true;
        }

    }
    public void SetSkillName(string sName)
    {
        skillName = sName;
        setSkill();
    }
    public string GetSkillName()
    {
        return skillName;
    }

    public void onClicked()
    {
        if (SkillDict[skillName].isLocked == false)
        {
            skillClicked.Raise(skillName);
            updateAllSkills.Raise();
            setSkill();
        }
    }

}

