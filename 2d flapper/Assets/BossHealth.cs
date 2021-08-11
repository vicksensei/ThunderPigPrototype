using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    IntValue bossHP;
    [SerializeField]
    IntValue bossCurrentHP;

    [Header("UI")]
    [SerializeField]
    Slider slider;

    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent projectileCol;
    [SerializeField]
    SOEvents.VoidEvent PlayerCol;
    [SerializeField]
    SOEvents.VoidEvent bossBloodied;
    [SerializeField]
    SOEvents.VoidEvent bossDead;
    

    bool bloodied = false;


    private void Awake()
    {
        bossCurrentHP.Value = bossHP.Value;
        UpdateHP();
    }

    public void UpdateHP()
    {
        slider.value = calcHealth();
        if (calcHealth() < .5f && !bloodied )
        {
            bossBloodied.Raise();
        }
        if(calcHealth() <= 0)
        {
            bossDead.Raise();
        }
    }

    float calcHealth()
    {
        return (float)bossCurrentHP.Value / (float)bossHP.Value;
    }

    public void TakeDamage(int howMuch)
    {
        bossCurrentHP.Value -= howMuch;
        UpdateHP();
    }

    public void HealDamage(int howMuch)
    {
        bossCurrentHP.Value += howMuch;
        UpdateHP();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            if (collision.GetComponent<projectile>() != null)
            {
                projectileCol.Raise();
                TakeDamage(collision.GetComponent<projectile>().damage);
            }
            else
            {
                PlayerCol.Raise();
            }
        }
        if (collision.tag == "Player") { }
    }
}
