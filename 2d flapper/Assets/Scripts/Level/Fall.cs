using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent PlayerDead;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerDead.Raise();
        }
    }
}
