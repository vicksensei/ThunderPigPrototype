using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsExplainer : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField]
    Animator animator;

    [Header("State")]
    [SerializeField]
    GameState gameState;

    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent backButton;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {

            animator.Play("UIFlap");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.Play("UIFire");
            //TODO Fire Animation
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.Play("UILeft");
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.Play("UIRight");
        }
    }

    

    public void OnBackButton()
    {
        backButton.Raise();
    }
}
