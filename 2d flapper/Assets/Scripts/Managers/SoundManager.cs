using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource sfxObject;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayClip(AudioClip clip, Transform spawnTransform, float volume)
    {
        //spawn gameobject
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);

        // assign audio clip
        audioSource.clip = clip;
        // assign volume
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);

    }


    public void PlayClipRandomPitch(AudioClip clip, Transform spawnTransform, float volume, float min, float max)
    {
        //spawn gameobject
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);

        // assign audio clip
        audioSource.clip = clip;
        // assign volume
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        //set pitch

        audioSource.pitch = (Random.Range(min, max));
        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);

    }
}
