using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    [SerializeField]
    MapNodeList map;
    [SerializeField]
    float speed;
    [SerializeField]
    ProgressionObject PO;

    bool isMoving = false;
    Vector3 target, nextTarget;
    int currentNodeIndex, destNodeIndex;
    int mult = 1;
    public void MoveToNode(int node)
    {
        if (!isMoving)
        { 
            isMoving = true;
            target = map.mapNodes[node].gameObject.transform.position;
            destNodeIndex = node;
            SetNextTarget();
        }
    }
    void SetNextTarget()
    {
        if (destNodeIndex > currentNodeIndex)
        {
            mult = 1;
            nextTarget = map.mapNodes[currentNodeIndex + mult].transform.position;
        }
        else if (destNodeIndex < currentNodeIndex)
        {
            mult = -1;
            nextTarget = map.mapNodes[currentNodeIndex +mult].transform.position;
        }
        else 
        {

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        currentNodeIndex = 0;
        transform.position = map.mapNodes[currentNodeIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextTarget, speed * Time.deltaTime);
        }
        if(transform.position == nextTarget)
        {
            currentNodeIndex = currentNodeIndex + mult;
            SetNextTarget();
        }
        if(transform.position == target) {
            isMoving = false;
        }
    }
}
