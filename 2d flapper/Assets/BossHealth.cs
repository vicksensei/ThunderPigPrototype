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
    [SerializeField]
    SOEvents.VoidEvent bossAngry;


    bool hasSuperArmor;
    bool bloodied = false;
    bool enraged = false;


    private void Awake()
    {
        bossCurrentHP.Value = bossHP.Value;
        UpdateHP();
        SuperArmorOn();
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
        if (!hasSuperArmor)
        {
            bossCurrentHP.Value -= howMuch;
            if (bossCurrentHP.Value < 0)
            {
                bossCurrentHP.Value = 0;
                bossDead.Raise();
            }

            if (bossCurrentHP.Value <= (bossHP.Value * 2 / 3) && !bloodied)
            {
                bossBloodied.Raise();
                bloodied = true;
            }
            if (bossCurrentHP.Value <= (bossHP.Value / 3) && !enraged)
            {
                bossAngry.Raise();
                enraged = true;
            }
            UpdateHP();
            // play a sound for damage
        }
        else
        {
            // play a sound for immunity
        }
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
                projectileCol.Raise();
            if (collision.GetComponent<projectile>() != null)
            {
                TakeDamage(collision.GetComponent<projectile>().damage);
            }
            else
            {
                TakeDamage(1);
            }

            Destroy(collision.gameObject);
        }
        if (collision.tag == "Player")
        {
                PlayerCol.Raise();
        }
    }

    public void SuperArmorOn()
    {
        hasSuperArmor = true;
        slider.gameObject.SetActive(false);
    }
    public void SuperArmorOff()
    {
        hasSuperArmor = false;
        slider.gameObject.SetActive(true);
    }
}
