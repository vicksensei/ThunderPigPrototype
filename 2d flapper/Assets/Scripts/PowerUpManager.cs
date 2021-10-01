using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField]
    GameObject LightningBall;
    [SerializeField]
    ProgressionObject PO;

    public void PickUpLightningBall() {
        Debug.Log("Lightning Ball picked up!");
        GameObject GO = Instantiate(LightningBall, transform.parent.position, Quaternion.identity, transform.parent);
    }
}
