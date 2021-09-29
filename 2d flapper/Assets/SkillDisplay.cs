using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    Dictionary<string, Skill> SkillDict;
    private void Start()
    {
        //setSkill();
    }

    public void setSkill()
    {
        SkillDict = current.GetSkillDict();
        Skill skill = SkillDict[skillName];
        skillNameText.text = skill.skillName;
        points.text = skill.points + "/" + skill.maxPoints;
        overlay.enabled = skill.isLocked;
        TT.text = skill.description;
        if (skill.isLocked)
        {
            TT.text += "\n [LOCKED]";
            foreach(SkillPreReq req in skill.skillPreReqs)
            {
                TT.text +="\n " + req.SkillName + ": " + SkillDict[req.SkillName].points + "/" + req.requiredPoints;
                if(SkillDict[req.SkillName].points >= req.requiredPoints)
                {
                    TT.text += " [DONE!]";
                }
            }
        }
        icon.sprite = current.skillsDB.skillsIconsDict()[skillName].skillIcon;

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

        Debug.Log("clicked on " + skillNameText.text);
    }

}

