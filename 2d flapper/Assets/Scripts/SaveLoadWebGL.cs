using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadWebGL : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SyncFiles();

    [DllImport("__Internal")]
    private static extern void WindowAlert(string message);
    [SerializeField]
    ProgressionObject saveFile;
    [SerializeField]
    ProgressionObject newSave;
    [SerializeField]
    SOEvents.VoidEvent gameReset;
    [SerializeField]
    SOEvents.VoidEvent gameLoaded;
    
    private void Start()
    {
        Load();
    }
       
    void Save(SaveFile gameDetails)
    {
        string dataPath = string.Format("{0}/SaveFile.dat", Application.persistentDataPath);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream;

        try
        {
            if (File.Exists(dataPath))
            {
                File.WriteAllText(dataPath, string.Empty);
                fileStream = File.Open(dataPath, FileMode.Open);
            }
            else
            {
                fileStream = File.Create(dataPath);
            }

            binaryFormatter.Serialize(fileStream, gameDetails);
            fileStream.Close();

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                SyncFiles();
            }
            PlatformSafeMessage("Saved Successfully!");
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Failed to Save: " + e.Message);
        }
    }

    public bool saveExists()
    {
        string dataPath = string.Format("{0}/SaveFile.dat", Application.persistentDataPath);

        try
        {
            if (File.Exists(dataPath))
            {
                return true;
            }
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Problem looking for save file: " + e.Message);
            return false;
        }
        return false;

    }

    void Load()
    {
        SaveFile gameDetails = null;
        string dataPath = string.Format("{0}/SaveFile.dat", Application.persistentDataPath);
        Debug.Log("Data path: " + dataPath);
        try
        {
            if (File.Exists(dataPath))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = File.Open(dataPath, FileMode.Open);

                gameDetails = (SaveFile)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                PlatformSafeMessage("Save successfully loaded!");
            }
            else
            {
                PlatformSafeMessage("Failed to Load: No save file found.");
                saveFile.Clone(newSave);
                Save(new SaveFile(saveFile));
                PlatformSafeMessage("Creating new save file instead.");
                gameLoaded.Raise();
            }
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Failed to Load: " + e.Message);
            saveFile.Clone(newSave);
            Save(new SaveFile(saveFile));
            PlatformSafeMessage("Creating new save file instead");
            gameLoaded.Raise();
        }

        saveFile.Load(gameDetails);
        gameLoaded.Raise();
    }

    private static void PlatformSafeMessage(string message)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            WindowAlert(message);
        }
        else
        {
            Debug.Log(message);
        }
    }

    public void OnResetButton()
    {
        saveFile.Clone(newSave);
        gameReset.Raise();
        Save(new SaveFile(saveFile));
    }
    public void OnSaveButton()
    {
        Save(new SaveFile(saveFile));
        //checkLoadButton();
    }

}
[Serializable]
public class SaveFile
{
    public int experiencePoints;

    public int currency;
    public int level;

    public int maxHP;
    public int maxCharge;
    public int curHP;
    public int curCharge;

    public float furthestDifficulty;
    public float currentDifficulty;

    public int highScore;
    public int curScore;

    public int flapsToCharge;
    public int pierceCount;
    public int numDrops;
    public int skillPoints;
    [SerializeField]
    public Skill[] skillsList;

    public SaveFile(ProgressionObject saveFile)
    {
        experiencePoints= saveFile.ExperiencePoints;

        currency = saveFile.Currency;
        level = saveFile.Level;

        maxHP = saveFile.MaxHP;
        maxCharge = saveFile.MaxCharge;
        curHP = saveFile.CurHP;
        curCharge = saveFile.CurCharge;

        furthestDifficulty = saveFile.FurthestDifficulty;
        currentDifficulty = saveFile.CurrentDifficulty;

        highScore = saveFile.HighScore;
        curScore = saveFile.CurScore;

        flapsToCharge = saveFile.FlapsToCharge;
        pierceCount = saveFile.PierceCount;
        numDrops = saveFile.NumDrops;
        skillPoints = saveFile.Skillpoints;
        skillsList = saveFile.SkillsList.ToArray();
    }
}
