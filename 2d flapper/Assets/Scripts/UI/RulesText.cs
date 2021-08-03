using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "-Avoid or Destroy enemies! \n" +
            "-Don't stop flapping! \n" +
            "-Get Ready!The game will Start once you click Start!";

    }
}             
