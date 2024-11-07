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

    [Header("Sounds")]
    [SerializeField]
    AudioClip FlapSound, FireSound;

    private void OnEnable()
    {
        Time.timeScale = 1;
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {

            animator.Play("UIFlap");
            SoundManager.instance.PlayClipRandomPitch(FlapSound, transform, 1f, 0.8f, 1f);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.Play("UIFire");
            SoundManager.instance.PlayClipRandomPitch(FireSound, transform, 1f, 0.8f, 1f);
            //TODO Fire Animation
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.Play("UILeft");
            SoundManager.instance.PlayClipRandomPitch(FlapSound, transform, 1f, 0.8f, 1f);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.Play("UIRight");
            SoundManager.instance.PlayClipRandomPitch(FlapSound, transform, 1f, 0.8f, 1f);
        }
    }



    public void OnBackButton()
    {
        backButton.Raise();
    }
}
