using SOEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    [SerializeField]
    string tagToDestroy;
    [SerializeField]
    VoidEvent killPerfection;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyThing(collision.collider);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyThing(collision);
    }

    void DestroyThing(Collider2D collision)
    {

        //Debug.Log("Collision with " + collision.name + " tagged as " + collision.gameObject.tag);
        if (collision.tag == tagToDestroy)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                EnemyDestroy destroyScript = collision.gameObject.GetComponent<EnemyDestroy>();
                Debug.Log(destroyScript);
                if (destroyScript != null)
                {
                    destroyScript.Escaped();
                }
            }
            if (collision.gameObject.tag == "Projectile")
            {
                projectile p = collision.gameObject.GetComponent<projectile>();
                if (p != null)
                {
                    if (p.IsMissedShot)
                    {
                        p.ResetCombo();
                    }
                }
            }
            Destroy(collision.gameObject);
        }
    }
}
