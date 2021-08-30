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

    public void Awake()
    {
        piercedSoFar = saveFile.PierceCount;
    }
    private void FixedUpdate()
    {
        if (isFromPlayer)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;
        }
    }

    public void DestroyMe()
    {
        Instantiate(DestroyParticle, transform.position, Quaternion.identity);
    }
    public void TryToDestroy()
    {
        if (isFromPlayer)
        {
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
