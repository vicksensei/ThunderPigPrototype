using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DropTable))]
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

    DropTable dt;
    private void Awake()
    {
        dt = GetComponent<DropTable>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        AnyCol.Raise();
        if (other.gameObject.tag == "Projectile")
        {
            projectileCol.Raise();
            dt.BeforeDestroy();
            other.gameObject.GetComponent<projectile>().TryToDestroy();
            Destroy(transform.parent.gameObject);
            
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
