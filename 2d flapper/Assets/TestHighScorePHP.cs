using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TestHighScorePHP : MonoBehaviour
{
    private string secretKey = "Pigwings";
    public string addScoreURL =
            "https://victorsensei.com/Games/ThunderPig/HSAdd.php";
    public Text nameTextInput;
    public Text scoreTextInput;

    public void SendScoreBtn()
    {/*
        StartCoroutine(PostScores(nameTextInput.text,
           Convert.ToInt32(scoreTextInput.text)));
        nameTextInput.gameObject.transform.parent.GetComponent<InputField>().text = "";
        scoreTextInput.gameObject.transform.parent.GetComponent<InputField>().text = "";*/
        StartCoroutine(DoPostScores(nameTextInput.text, Convert.ToInt32(scoreTextInput.text)));
    }


    IEnumerator DoPostScores(string name, int score)
    {
        WWWForm form = new WWWForm();

        string hashPost = Md5Sum(name + score + secretKey);

        form.AddField("namePost", name);
        form.AddField("scorePost", score);
        form.AddField("hashPost", hashPost);

        using (UnityWebRequest www = UnityWebRequest.Post(addScoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError|| www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.result);
                Debug.Log("Successfully posted score!");
            }
        }
    }

    public string Md5Sum(string strToEncrypt)
    {

        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');

    }
}


