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
    BoolValue playerIsImmune;
    [SerializeField]
    ProgressionObject saveFile;


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


    [Header("Prefabs")]
    [SerializeField]
    GameObject DestroyParticle;
    [SerializeField]
    GameObject ImmuneParticle;

    bool hasSuperArmor;
    bool bloodied = false;
    bool enraged = false;

    public int currentHP, maximumHP;

    private void Awake()
    {
        maximumHP = bossHP.Value * (int)saveFile.CurrentDifficulty;
        currentHP = maximumHP;
        UpdateHP();
        SuperArmorOn();
    }

    public void UpdateHP()
    {
        slider.value = calcHealth();
    }

    float calcHealth()
    {
        return (float)currentHP / (float)maximumHP;
    }

    public void TakeDamage(int howMuch)
    {
        if (!hasSuperArmor)
        {
            currentHP -= howMuch;
            if (currentHP < 0)
            {
                currentHP = 0;
                bossDead.Raise();
            }

            if (currentHP <= (maximumHP * 2 / 3) && !bloodied)
            {
                bossBloodied.Raise();
                bloodied = true;
            }
            if (currentHP <= (maximumHP / 3) && !enraged)
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
        currentHP += howMuch;
        if (currentHP > maximumHP)
        {
            currentHP = maximumHP;
        }
        UpdateHP();
    }
    void ShowImmuneParticle()
    {
        Instantiate(ImmuneParticle, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
                projectileCol.Raise();
            if (collision.GetComponent<projectile>() != null)
            {
                TakeDamage(collision.GetComponent<projectile>().damage + saveFile.GetSkillDict()["Damage"].points);
            }
            else
            {
                TakeDamage(1 + saveFile.GetSkillDict()["Damage"].points);
            }

            if (hasSuperArmor)
            {
                ShowImmuneParticle();
            }
            else
            {
                collision.gameObject.GetComponent<projectile>().ShowHitParticle();
            }

            Destroy(collision.gameObject);
        }
        if (collision.tag == "Player")
        {
            Debug.Log("Collision! Immune? " + playerIsImmune.Value);
            if (!playerIsImmune.Value)
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
