using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed;
    public int damage;

    [Header("Prefabs")]
    [SerializeField]
    GameObject DestroyParticle;
    [SerializeField]
    SOEvents.VoidEvent PlayerCol;

    [Header("Values")]
    [SerializeField]
    bool isFromPlayer;
    [SerializeField]
    ProgressionObject saveFile;
    int piercedSoFar;
    [SerializeField]
    Vector3 direction = Vector3.right;
    [SerializeField]
    bool isMoving = true;
    [SerializeField]
    bool Destroyable = true;

    public bool IsFromPlayer { get => isFromPlayer; set => isFromPlayer = value; }

    public void Awake()
    {
        piercedSoFar = saveFile.PierceCount + saveFile.GetSkillDict()["Pierce"].points;
        if (IsFromPlayer)
        {
            damage += saveFile.SkillsList[2].points;
        }
        else
        {
            damage += (int)saveFile.CurrentDifficulty - 1;
            speed += (saveFile.CurrentDifficulty - 1) * 5;
            direction = Vector3.left;
        }
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position += direction * speed * Time.fixedDeltaTime;
        }
    }

    public void ShowHitParticle()
    {
        Instantiate(DestroyParticle, transform.position, Quaternion.identity);
    }

    public void TryToDestroy()
    {
        if (Destroyable)
        {
            if (IsFromPlayer)
            {
                ShowHitParticle();
                if (piercedSoFar <= 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    piercedSoFar--;

                }
            }
            else
            {
                ShowHitParticle();
                Destroy(gameObject);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (!IsFromPlayer)
            {
                PlayerCol.Raise();
                TryToDestroy();
            }
        }
    }


}
