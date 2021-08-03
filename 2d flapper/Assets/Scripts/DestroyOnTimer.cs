
using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    [SerializeField]
    float timer;
    void Awake()
    {

        Destroy(gameObject, timer);
    }
}
