using System.Collections;
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
    [Header("Values")]
    [SerializeField]
    BoolValue playerIsImmune;


    private void OnTriggerEnter2D(Collider2D other)
    {
        AnyCol.Raise();
        if (other.gameObject.tag == "Projectile")
        {
            projectileCol.Raise();
            other.gameObject.GetComponent<projectile>().TryToDestroy();
            Destroy(gameObject);
            
        }
        if (other.gameObject.tag == "Player")
        {
            if (!playerIsImmune.Value)
            {
                PlayerCol.Raise();
            }
        }
    }

}
