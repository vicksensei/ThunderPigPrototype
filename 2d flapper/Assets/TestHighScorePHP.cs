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
            "https://victorsensei.com/Games/ThunderPig/HSTest.php";
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
        form.AddField("post_leaderboard", "true");
        form.AddField("Name", name);
        form.AddField("Score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(addScoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError|| www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Successfully posted score!");
            }
        }
    }
}


