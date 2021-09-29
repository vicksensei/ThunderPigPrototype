using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillsManager : MonoBehaviour
{
    [SerializeField]
    ProgressionObject currentProgression;
    [SerializeField]
    GameObject SkillDisplay;
    [SerializeField]
    Transform childContainer;

    List<GameObject> displays;
    Dictionary<string, SkillDisplay> skillsDispDict;
    Dictionary<string, Skill> skillDict;

    private void Start()
    {
        skillDict = currentProgression.GetSkillDict();
        RefreshSkills();
        SortSkills();
    }

    public void RefreshSkills()
    {
        skillsDispDict = new Dictionary<string, SkillDisplay>();
        for (int i = 0; i < currentProgression.SkillsList.Count; i++)
        {
            GameObject GO = Instantiate(SkillDisplay, childContainer);
            string sName = currentProgression.SkillsList[i].skillName;
            CheckPreReq(sName);
            GO.GetComponent<SkillDisplay>().SetSkillName(sName);
            skillsDispDict.Add(sName, GO.GetComponent<SkillDisplay>());
        }
    }

    void SortSkills()
    {
        foreach(KeyValuePair<string, SkillDisplay> SD in skillsDispDict)
        {// check every Skill Display
            //get their name
            string myName = SD.Key;
            // and their value
            SkillDisplay sDisplay = SD.Value;
            //if the skill with myName HAS pre-requisites
            if (skillDict[myName].skillPreReqs.Length != 0)
            {
                //Get the name of the first prerequisite
                string nameOfFirstPreReq = skillDict[myName].skillPreReqs[0].SkillName;
                //get the Display container's transform that maatches the name of the first prerequisite
                SD.Value.transform.SetParent(skillsDispDict[nameOfFirstPreReq].childContainer);
                //Make that transform the parent of the Display we're checking.
            }
        }
    }

    void CheckPreReq(string SkillName)
    {
        int count = 0;
        foreach(SkillPreReq req in skillDict[SkillName].skillPreReqs)
        {
            if(skillDict[req.SkillName].points >= req.requiredPoints){
                count++;
            }
        }
        if (count == skillDict[SkillName].skillPreReqs.Length)
        {
            skillDict[SkillName].isLocked = false;
        }
    }

    void LevelUpSkill(string SkillName)
    {
        if(currentProgression.Skillpoints > 0 && skillDict[SkillName].points < skillDict[SkillName].maxPoints)
        {
            currentProgression.Skillpoints--;
            skillDict[SkillName].points++;
        }
    }
}
