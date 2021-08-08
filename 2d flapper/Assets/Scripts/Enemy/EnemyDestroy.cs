﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{

    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent projectileCol;
    [SerializeField]
    SOEvents.VoidEvent PlayerCol;
    [SerializeField]
    SOEvents.VoidEvent AnyCol;


    [Header("Prefabs")]
    [SerializeField]
    GameObject DestroyParticle;


    private void OnTriggerEnter2D(Collider2D other)
    {
        AnyCol.Raise();
        if (other.gameObject.tag == "Projectile")
        {
            if (DestroyParticle != null) ;
                Instantiate(DestroyParticle, other.transform.position, Quaternion.identity);
            projectileCol.Raise();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            PlayerCol.Raise();
        }
    }

}
