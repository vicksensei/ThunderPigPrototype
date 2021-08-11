using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    [SerializeField]
    string tagToDestroy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == tagToDestroy)
        {
            Destroy(collision.gameObject);
        }
    }
}
