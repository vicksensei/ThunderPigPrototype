using SOEvents;
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

    [SerializeField]
    bool canMiss = true;
    [Header("Events")]
    [SerializeField]
    VoidEvent clearCombo;
    [SerializeField]
    VoidEvent AddCombo;
    bool isMissedShot = true;

    public bool IsFromPlayer { get => isFromPlayer; set => isFromPlayer = value; }
    public bool IsMissedShot { get => isMissedShot; set => isMissedShot = value; }

    public void Awake()
    {
        if (!canMiss) { isMissedShot = false; }
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
    public void HitSomething()
    {
        if (canMiss && isFromPlayer)
        {
            isMissedShot = false;
            AddCombo.Raise();
        }
    }
    public void ResetCombo()
    {
        clearCombo.Raise();
    }
    public void TryToDestroy()
    {
        if (Destroyable)
        {
            if (IsFromPlayer)
            {
                ShowHitParticle();
                if (canMiss == true && isMissedShot == true)
                {
                    clearCombo.Raise();
                }
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(name + " triggered " + collision.gameObject.name);
        doCollision(collision);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(name + " hit " + other.gameObject.name);
        doCollision(other.collider);
    }

    void doCollision(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            if (!IsFromPlayer)
            {
                PlayerCol.Raise();
                TryToDestroy();
            }
        }
    }

}
