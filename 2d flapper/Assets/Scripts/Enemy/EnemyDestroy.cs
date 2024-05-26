using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    [SerializeField]
    TMP_Text HPText;
    [Header("Values")]
    [SerializeField]
    int maxHP = 1;
    int curHP;
    [SerializeField]
    bool HPScales = true;
    [SerializeField]
    BoolValue playerIsImmune;
    [SerializeField]
    int ExpValue = 1;
    [Header("Prefabs")]
    [SerializeField]
    GameObject DestroyParticle;
    [SerializeField]
    GameObject ImmuneParticle;
    [SerializeField]
    ProgressionObject saveFile;

    DropTable dt;
    private void Awake()
    {
        dt = GetComponent<DropTable>();
        /*if (HPBar != null)
        {
            HPBar.gameObject.SetActive(true);
        }*/
    }

    private void Start()
    {
        if (HPScales)
        {
            maxHP += (int)(saveFile.CurrentDifficulty - 1);
            Debug.Log("Max HP: " + maxHP.ToString());
            Debug.Log(saveFile.CurrentDifficulty.ToString());
        }
        curHP = maxHP;
        if (HPBar != null)
        {
            RefreshHPUI();
            HPBar.gameObject.SetActive(false);
        }

    }

    void RefreshHPUI()
    {
        HPBar.maxValue = maxHP;
        HPBar.value = (float)curHP;
        HPText.text = curHP + "/" + maxHP;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AnyCol.Raise();
        if (other.gameObject.tag == "Projectile")
        {
            if (other.GetComponent<projectile>().IsFromPlayer)
            {
                projectileCol.Raise();
                other.gameObject.GetComponent<projectile>().ShowHitParticle();
                curHP -= other.gameObject.GetComponent<projectile>().damage;
                other.gameObject.GetComponent<projectile>().TryToDestroy();
                if (curHP <= 0)
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
                        RefreshHPUI();
                        HPBar.gameObject.SetActive(true);
                    }
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
