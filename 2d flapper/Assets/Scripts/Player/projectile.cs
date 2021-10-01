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


    public void Awake()
    {
        piercedSoFar = saveFile.PierceCount + saveFile.GetSkillDict()["Pierce"].points;
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position += direction * speed * Time.deltaTime;
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
            if (isFromPlayer)
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
        }
    }


}
