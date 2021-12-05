using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowScroll : MonoBehaviour
{
    [SerializeField]
    public float layer;
    [SerializeField]
    Transform leftEdge;
    [SerializeField]
    Transform rightEdge;
    [SerializeField]
    Transform multiplier;

    [SerializeField]
    GameState GS;

    float offset;

    bool isFirst = true;
    bool isMoving = true;
    // Start is called before the first frame update
    void Start()
    {
        offset = GetComponent<SpriteRenderer>().bounds.size.x;
        if(GS.state == GameState.State.Waiting) { StopMoving(); }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            if (GS.state == GameState.State.Waiting) { StopMoving(); }
            transform.position -= new Vector3(layer * .25f * Time.deltaTime, 0);
            if (isFirst)
            {
                if (leftEdge.position.x < multiplier.position.x)
                {
                    Vector3 pos = new Vector3(transform.position.x + offset -.01f, transform.position.y);
                    Instantiate(gameObject, pos, Quaternion.identity, transform.parent);
                    isFirst = false;
                }
            }
            else
            {
                if (rightEdge.position.x < multiplier.position.x)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }
    public void StopMoving()
    {
        isMoving = false;
    }
}
