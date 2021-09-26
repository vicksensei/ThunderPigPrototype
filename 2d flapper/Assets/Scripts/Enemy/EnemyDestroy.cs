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
    [SerializeField]
    SOEvents.IntEvent GiveXP;
    [Header("Values")]
    [SerializeField]
    BoolValue playerIsImmune;
    [SerializeField]
    int ExpValue =1;
    [Header("Prefabs")]
    [SerializeField]
    GameObject DestroyParticle;
    [SerializeField]
    GameObject ImmuneParticle;

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
            other.gameObject.GetComponent<projectile>().ShowHitParticle();
            other.gameObject.GetComponent<projectile>().TryToDestroy();
            GiveXP.Raise(ExpValue); ShowHitParticle();
            Destroy(transform.parent.gameObject);
            Debug.Log("collision with " + other.gameObject.name);
            
        }
        if (other.gameObject.tag == "Player")
        {
            if (!playerIsImmune.Value)
            {
                PlayerCol.Raise();
            }
        }
    }

    public void ShowHitParticle()
    {
        Instantiate(DestroyParticle, transform.position, Quaternion.identity);
    }
}
