using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    [SerializeField]
    SOEvents.VoidEvent raiseOnDestroy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            raiseOnDestroy.Raise();
            Destroy(transform.parent.gameObject);
        }
    }
}
