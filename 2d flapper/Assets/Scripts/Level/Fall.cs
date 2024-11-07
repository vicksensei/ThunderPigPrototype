using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent PlayerDead;

    [Header("Sounds")]
    [SerializeField]
    AudioClip DeathSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlayClip(DeathSound, transform, 1f);
            PlayerDead.Raise();
        }
    }
}
