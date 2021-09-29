using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SaveState", menuName = "SOValues/SaveState")]
public class ProgressionObject : ScriptableObject
{
    [SerializeField]
    int experiencePoints;

    [SerializeField]
    int currency;
    [SerializeField]
    int level;

    [SerializeField]
    int maxHP;
    [SerializeField]
    int maxCharge;
    [SerializeField]
    int curHP;
    [SerializeField]
    int curCharge;
    [SerializeField]
    int flapsToCharge;

    [SerializeField]
    int pierceCount;
    [SerializeField]
    int numDrops;

    [SerializeField]
    float furthestDifficulty;
    [SerializeField]
    float currentDifficulty;


    [SerializeField]
    int highScore;

    [SerializeField]
    int curScore;

    public SkillDb skillsDB;

    [SerializeField]
    int skillpoints;

    List<Skill> skillsList;

    public int ExperiencePoints { get => experiencePoints; set => experiencePoints = value; }
    public int Currency { get => currency; set => currency = value; }
    public int Level { get => level; set => level = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int MaxCharge { get => maxCharge; set => maxCharge = value; }
    public int CurHP { get => curHP; set => curHP = value; }
    public int CurCharge { get => curCharge; set => curCharge = value; }
    public float FurthestDifficulty { get => furthestDifficulty; set => furthestDifficulty = value; }
    public float CurrentDifficulty { get => currentDifficulty; set => currentDifficulty = value; }
    public int HighScore { get => highScore; set => highScore = value; }
    public int CurScore { get => curScore; set => curScore = value; }
    public int FlapsToCharge { get => flapsToCharge; set => flapsToCharge = value; }
    public int PierceCount { get => pierceCount; set => pierceCount = value; }
    public int NumDrops { get => numDrops; set => numDrops = value; }
    public int Skillpoints { get => skillpoints; set => skillpoints = value; }
    public List<Skill> SkillsList { get => skillsList; set => skillsList = value; }

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public void Clone(ProgressionObject original)
    {
        experiencePoints= original.experiencePoints;
        level= original.level;

        currency= original.currency;
    
        maxHP= original.maxHP;
        curHP= original.curHP;

        maxCharge= original.maxCharge;
        curCharge= original.curCharge;
        flapsToCharge= original.flapsToCharge;

        pierceCount= original.pierceCount;
        numDrops= original.numDrops;

        furthestDifficulty= original.furthestDifficulty;
        currentDifficulty= original.currentDifficulty;

        highScore= original.highScore;
        curScore= original.curScore;

        SkillsList = skillsDB.skillsList;
        skillpoints = original.skillpoints;
    }
    public void Load(SaveFile saveFile)
    {
        experiencePoints = saveFile.experiencePoints;
        level = saveFile.level;

        currency = saveFile.currency;

        maxHP = saveFile.maxHP;
        curHP = saveFile.curHP;

        maxCharge = saveFile.maxCharge;
        curCharge = saveFile.curCharge;
        flapsToCharge = saveFile.flapsToCharge;


        pierceCount = saveFile.pierceCount;
        numDrops = saveFile.numDrops;

        furthestDifficulty = saveFile.furthestDifficulty;
        currentDifficulty = saveFile.currentDifficulty;

        highScore = saveFile.highScore;
        curScore = saveFile.curScore;

        SkillsList.Clear();
        SkillsList.AddRange(saveFile.skillsList);
        skillpoints = saveFile.skillPoints;

    }

    public Dictionary<string, Skill> GetSkillDict()
    {
        Dictionary<string, Skill> Temp = new Dictionary<string, Skill> { };

        foreach (Skill skill in SkillsList)
        {
            Temp.Add(skill.skillName, skill);
        }

        return Temp;
    }


}
