using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    private AudioClip AudioClipToPlay;
    [SerializeField]
    private bool randomize = true;
    [SerializeField]
    private float min = 0.7f, max = 1.0f;
    public void play()
    {
        if (randomize == true)
        {
            SoundManager.instance.PlayClipRandomPitch(AudioClipToPlay, transform, 1f, min, max);
        }
        else
        {
            SoundManager.instance.PlayClip(AudioClipToPlay, transform, 1f);
        }
    }
}
