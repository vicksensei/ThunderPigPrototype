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


    [Header("Sounds")]
    [SerializeField]
    AudioClip DeathSound;
    [SerializeField]
    AudioClip HurtSound;
    [SerializeField]
    AudioClip ImmuneSound;


    bool hasSuperArmor;
    bool bloodied = false;
    bool enraged = false;
    bool dead = false;

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
            if (currentHP <= 0 && !dead)
            {
                currentHP = 0;
                SoundManager.instance.PlayClip(DeathSound, transform, 1f);
                bossDead.Raise();
                dead = true;
                gameObject.SetActive(false);
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
            SoundManager.instance.PlayClip(HurtSound, transform, 1f);
        }
        else
        {
            SoundManager.instance.PlayClip(ImmuneSound, transform, 1f);
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
            projectile p = collision.gameObject.GetComponent<projectile>();

            projectileCol.Raise();
            if (p != null)
            {
                p.HitSomething();
                TakeDamage(p.damage + saveFile.GetSkillDict()["Damage"].points);

                if (hasSuperArmor)
                {
                    ShowImmuneParticle();
                    Destroy(collision.gameObject);
                }
                else
                {
                    p.ShowHitParticle();
                    p.TryToDestroy();
                }
            }
            else
            {
                TakeDamage(1 + saveFile.GetSkillDict()["Damage"].points);
            }

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
