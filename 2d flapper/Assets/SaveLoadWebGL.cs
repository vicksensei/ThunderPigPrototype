using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Networking;

public class SaveLoadWebGL : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SyncFiles();

    [DllImport("__Internal")]
    private static extern void WindowAlert(string message);
    [SerializeField]
    ProgressionObject saveFile;

    private void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 25;
        /*  if(GUI.Button(new Rect(10,10,450,80), "Send to file", buttonStyle))
          {
              StartCoroutine(SendTextToFile());
          }
          if (GUI.Button(new Rect(10, 100, 450, 80), "Get from file", buttonStyle))
          {
              StartCoroutine(GetTextFromFile());
          }*/

        if (GUI.Button(new Rect(10, 10, 450, 80), "Save", buttonStyle))
        {
            Save(new SaveFile(saveFile));
        }
        if (GUI.Button(new Rect(10, 100, 450, 80), "Load", buttonStyle))
        {
            Load();
        }
    }
    /*
    IEnumerator SendTextToFile()
    {
        WWWForm form = new WWWForm();
        form.AddField("test", JsonUtility.ToJson(new SaveFile(saveFile)));
        WWW www = new WWW("http://victorsensei.com/Games/ThunderPig/Test/serverfromunity.php", form);
        

        yield return www;
        if (www.error != null)
        {
            Debug.Log("failure");
        }
        else { Debug.Log(www.text); }
    }



    IEnumerator GetTextFromFile()
    {
        WWWForm form = new WWWForm();
        WWW www = new WWW("http://victorsensei.com/Games/ThunderPig/Test/servertounity.php", form);
        //DownloadHandler DLH;
        //UnityWebRequest UWH = new UnityWebRequest("http://victorsensei.com/Games/ThunderPig/Test/servertounity.php");
        yield return www;
        if (www.error != null)
        {
            Debug.Log("failure");
        }
        else { Debug.Log(www.text); }
    } */


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
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Failed to Save: " + e.Message);
        }
    }

    void Load()
    {
        SaveFile gameDetails = null;
        string dataPath = string.Format("{0}/SaveFile.dat", Application.persistentDataPath);

        try
        {
            if (File.Exists(dataPath))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = File.Open(dataPath, FileMode.Open);

                gameDetails = (SaveFile)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Failed to Load: " + e.Message);
        }

        saveFile.Load(gameDetails);
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
    }
}
