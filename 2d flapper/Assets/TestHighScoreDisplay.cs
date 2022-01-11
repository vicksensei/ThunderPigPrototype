using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TestHighScoreDisplay : MonoBehaviour
{
    [SerializeField]
        Text scoreText;
    string urlDisplay = "http://vickrpgcom.ipage.com/Games/ThunderPig/HSDisplay.php";
    // Start is called before the first frame update
    void Start()
    {
        GetScores(); 
    }

    public void GetScores()
    {
        scoreText.text = "Loading Scores";

        StartCoroutine(GetRequest(urlDisplay));

    }

    IEnumerator GetRequest(string URL)
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
                    Debug.LogError(pages[page] + ": HTTP Error: " );
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
                    scoreText.text = temp;
                    break;
                default:
                    scoreText.text = "something Unexpected Happened";
                    break;
            }
        }
    }
}

/*
function getScores()
{
    // First a loading message
    scoreText.text = "Loading Scores";
    // Start a download of the given URL
    var wwwDisplay : WWW = new WWW(urlDisplay);
    // Wait for download to complete
    yield wwwDisplay;
    // if it can't load the URL
    if (wwwDisplay.error)
    {
        // Write in the console: There was an error getting the high score: Could not resolve host: xxx; No data record of requested type
        print("There was an error getting the high score: " + wwwDisplay.error);
        // Display an error message
        scoreText.text = "No Data Record";
    }
    else
    {
        // This is a GUIText that will display the scores in game
        scoreText.text = wwwDisplay.text;
    }
}
// Get Score END ###################################################################  
*/