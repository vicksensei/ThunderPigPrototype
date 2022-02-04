using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class InputReader : MonoBehaviour
{
    [SerializeField]
    TMP_Text Output;
    [SerializeField]
    TMP_Text scoreText;
    [SerializeField]
    GameObject keyboard;
    [SerializeField]
    GameObject closeButton;
    [SerializeField]
    ProgressionObject current;

    string addScoreURL =
        "https://victorsensei.com/Games/ThunderPig/HSAdd.php";

    string displayScoreURL = 
        "https://vickrpgcom.ipage.com/Games/ThunderPig/HSDisplay.php";

    void Start()
    {
        Output.text = "";
        scoreText.text = "";
        StartCoroutine(GetScores(displayScoreURL));
        keyboard.SetActive(true);
        closeButton.SetActive(false);
    }

    public void onKeyEvent(string s)
    {
        //Debug.Log(s);
        if (s.Length == 1)
        {
            if (Output.text.Length < 10)
            {
                Output.text += s;
            }
        }
        else
        {
            if (s == "backspace")
            {
                if (Output.text.Length > 0)
                {
                    Output.text = Output.text.Substring(0, Output.text.Length - 1);
                }
            }
            if (s == "OK")
            {
                if (Output.text.Trim() != "")
                {
                    StartCoroutine(ScoreProcess());
                }
            }
        }
    }

    IEnumerator ScoreProcess()
    {
        StartCoroutine(DoPostScores(Output.text, current.HighScore));
        keyboard.SetActive(false);
        closeButton.SetActive(true);
        yield return new WaitForSeconds(2);
        StartCoroutine(GetScores(displayScoreURL));
    }

    IEnumerator DoPostScores(string name, int score)
    {
        WWWForm form = new WWWForm();


        form.AddField("namePost", name);
        form.AddField("scorePost", score);
        //form.AddField("hashPost", hashPost);

        using (UnityWebRequest www = UnityWebRequest.Post(addScoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
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

    IEnumerator GetScores(string URL)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
        {//
            yield return webRequest.SendWebRequest();
            string[] pages = URL.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    scoreText.text = "Data/Connection Error: " + webRequest.error;
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: ");
                    scoreText.text = "HTTP Error: " + webRequest.error;
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    string temp = webRequest.downloadHandler.text;
                    temp = temp.Replace("<table>", "");
                    temp = temp.Replace("<tr>", "");
                    temp = temp.Replace("<td>", "");
                    temp = temp.Replace("</table>", "");
                    temp = temp.Replace("</tr>", "");
                    temp = temp.Replace("</td>", "");
                    string[] lines = temp.Split('\n');

                    temp = "";
                    string curline;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if ( i % 2 == 0 && i+2 <= lines.Length)
                        {
                            curline = lines[i] +"<pos=75%>" +lines[i+1];
                            if (lines[i].Trim() == Output.text)
                            {
                                curline = "<b>" + curline + "</b>";
                            }
                            temp += curline + "\n";
                        }
                    }
                    scoreText.text = temp;
                    break;
                default:
                    scoreText.text = "something Unexpected Happened";
                    break;
            }
        }
    }
}

