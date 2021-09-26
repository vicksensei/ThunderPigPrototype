using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField]
    GameObject LightningBall;

    public void PickUpLightningBall() {
        Debug.Log("Lightning Ball picked up!");
        GameObject GO = Instantiate(LightningBall, transform.parent.position, Quaternion.identity, transform.parent);
        //GO.GetComponent<CircleMove>().SetOrbiter(transform.parent);
        GO.GetComponent<DestroyOnTimer>().SetTimer(2f);
    }
}
