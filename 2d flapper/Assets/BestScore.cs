using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    [SerializeField]
    ScoreManager SM;
    [SerializeField]
    GameObject HS;
    // Start is called before the first frame update
    void Start()
    {
        CheckHighScore();
    }

    public void CheckHighScore()
    {
        if (SM.newHigh) { HS.SetActive(true); }
        else { HS.SetActive(false); }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
