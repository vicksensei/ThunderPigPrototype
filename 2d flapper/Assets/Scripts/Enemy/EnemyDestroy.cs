using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [Header("UI")]
    [SerializeField]
    Slider HPBar;
    [Header("Values")]
    [SerializeField]
    int maxHP;
    int curHP;
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
        curHP = maxHP;
        if (HPBar != null)
        {
            HPBar.gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        AnyCol.Raise();
        if (other.gameObject.tag == "Projectile")
        {
            projectileCol.Raise();
            other.gameObject.GetComponent<projectile>().ShowHitParticle();
            curHP-= other.gameObject.GetComponent<projectile>().damage;
            other.gameObject.GetComponent<projectile>().TryToDestroy();
            if (curHP < 0)
            {
                dt.BeforeDestroy();
                GiveXP.Raise(ExpValue); ShowHitParticle();
                Destroy(transform.parent.gameObject);
                //Debug.Log("collision with " + other.gameObject.name);
            }
            else
            {
                if (HPBar != null)
                {
                    HPBar.value = (float)curHP / (float)maxHP;
                    HPBar.gameObject.SetActive(true);
                }
            }
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

    public void ShowDestroyParticle()
    {
        Instantiate(DestroyParticle, transform.position, Quaternion.identity);
    }
}
