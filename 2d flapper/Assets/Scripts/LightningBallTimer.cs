
using UnityEngine;

public class LightningBallTimer : MonoBehaviour
{
    [SerializeField]
    ProgressionObject PO;
    [SerializeField]
    float timer =3;

    public void SetTimer(float nt) { timer = nt; }
    void Awake()
    {

        Destroy(gameObject, timer + PO.GetSkillDict()["Lightning Power"].points);
    }
}
