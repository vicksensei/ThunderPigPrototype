using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    float duration=2;
    Animator animator;

    SceneLoader SL;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SL = GetComponent < SceneLoader > ();
        TurnBlack();
        Invoke("FadeIn", duration);
    }
    public void FadeIn() {
        animator.Play("transitionIn");
    }
    public void FadeOut()
    {
        animator.Play("transitionOut");
    }
    public void TurnBlack()
    {
        animator.Play("Black");
    }
    public void TurnOff()
    {
        animator.Play("Gone");
    }
}
