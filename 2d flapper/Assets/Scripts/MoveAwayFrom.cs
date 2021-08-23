using UnityEngine;

public class MoveAwayFrom : MonoBehaviour
{
    [SerializeField]
    float distance;
    [SerializeField]
    string targetTag;
    [SerializeField]
    float speed;

    Transform target;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == targetTag)
        {
            target = collision.gameObject.transform;
            if (target.position == transform.position)
            {
                target.position += (Vector3)Random.insideUnitCircle * .001f ;
            }else if (Vector3.Distance(target.position, transform.position) <= distance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, -1 * speed * Time.deltaTime);
            }
        }
    }
}
