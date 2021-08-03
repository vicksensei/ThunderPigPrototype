using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent PlayerDead;
    [SerializeField]
    SOEvents.VoidEvent StopGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerDead.Raise();
        }
    }
}
