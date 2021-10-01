using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SOEvents;

public class SkillsManager : MonoBehaviour
{
    [Header("Progression")]
    [SerializeField]
    ProgressionObject currentProgression;

    [Header("Components")]
    [SerializeField]
    Transform childContainer;
    [SerializeField]
    Transform lineparent;

    [Header("Prefabs")]
    [SerializeField]
    GameObject SkillDisplay;
    [SerializeField]
    GameObject linePrefab;


    [Header("Events")]
    [SerializeField]
    VoidEvent Refresh;

    List<GameObject> displays;
    Dictionary<string, SkillDisplay> skillsDispDict;
    Dictionary<string, Skill> skillDict;

    void Start()
    {
        skillDict = currentProgression.GetSkillDict();
        GenerateSkillDisplays();
        SortSkills();
    }
    
    void GenerateSkillDisplays()
    {
        skillsDispDict = new Dictionary<string, SkillDisplay>();
        for (int i = 0; i < currentProgression.SkillsList.Count; i++)
        {
            GameObject GO = Instantiate(SkillDisplay, childContainer);
            string sName = currentProgression.SkillsList[i].skillName;
            CheckPreReq(sName);
            Debug.Log(sName);
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
                for (int i = 0; i < skillDict[myName].skillPreReqs.Length; i++)
                {
                    //Get the name of the prerequisite
                    string nameOfPreReq = skillDict[myName].skillPreReqs[i].SkillName;
                    //if that name is in our list
                    if (skillsDispDict.ContainsKey(nameOfPreReq))
                    {
                        if (i == 0)
                        { 
                            //get the Display container's transform that maatches the name of the first prerequisite on the list
                            SD.Value.transform.SetParent(skillsDispDict[nameOfPreReq].childContainer);
                            //Make that transform the parent of the Display we're checking.
                        }
                        //generate Lines from point A to point B
                        GameObject GO = Instantiate(linePrefab, lineparent);
                        GO.GetComponent<PointRotator>().point1 = skillsDispDict[myName].gameObject.transform;
                        GO.GetComponent<PointRotator>().point2 = skillsDispDict[nameOfPreReq].gameObject.transform;
                        GO.GetComponent<PointRotator>().Reposition();
                    }
                }
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

    public void LevelUpSkill(string SkillName)
    {
        skillDict = currentProgression.GetSkillDict();
        if (currentProgression.Skillpoints > 0 && skillDict[SkillName].points < skillDict[SkillName].maxPoints)
        {
            currentProgression.Skillpoints--;
            skillDict[SkillName].points++;
        }
        Refresh.Raise();
    }
}
