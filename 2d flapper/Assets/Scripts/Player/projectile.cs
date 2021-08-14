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


    private void Update()
    {
        transform.position +=  Vector3.right * speed * Time.deltaTime;
    }

    public void DestroyMe()
    {
        Instantiate(DestroyParticle, transform.position, Quaternion.identity);
    }


}
