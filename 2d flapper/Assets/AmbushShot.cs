using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof( EnemyAmbush))]
public class AmbushShot : MonoBehaviour
{
    [SerializeField]
    float shootRate;

    [SerializeField]
    GameObject ProjectilePrefab;
    [SerializeField]
    Transform shootPoint;
    [SerializeField]
    ProgressionObject PO;

    [SerializeField]
    bool canShootImmediately = true;
    EnemyAmbush EA;
    float shoottimer = 0;
    void Start()
    {
        EA = GetComponent<EnemyAmbush>();
        shootRate -= (PO.CurrentDifficulty * .2f);
        if(shootRate <= .5f) { shootRate = .5f; }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (EA.CanShoot())
        {
            shoottimer += Time.fixedDeltaTime;
            if(shoottimer >= shootRate)
            {
                Shoot();
            }
        }
        else
        {
            if (canShootImmediately)
            {
                shoottimer = shootRate;
            }
        }
    }

    void Shoot()
    {
        shoottimer = 0;
        Instantiate(ProjectilePrefab, shootPoint.position, Quaternion.identity);

    }
}
