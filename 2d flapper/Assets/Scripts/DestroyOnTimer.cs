
using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    [SerializeField]
    float timer;

    public void SetTimer(float nt) { timer = nt; }
    void Awake()
    {

        Destroy(gameObject, timer);
    }
}
