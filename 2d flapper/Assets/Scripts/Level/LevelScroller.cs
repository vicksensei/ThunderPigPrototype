using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScroller : MonoBehaviour
{

    [Header("Value Objects")]
    [SerializeField]
    FloatValue ScrollSpeed;

    [Header("State")]
    [SerializeField]
    GameState gameState;
    [Header("Switch")]
    [SerializeField]
    bool canScrollNow;
    
    // Update is called once per frame
    void Update()
    {
        if (canScrollNow && gameState.state == GameState.State.Playing)
        {
            Vector3 newPos = new Vector3(1 * ScrollSpeed.Value * Time.deltaTime, 0, 0);
            if (checkCircleMover())
            {
                GetComponent<CircleMoveEnemy>().Center -= newPos;
            }
            else
                transform.position -= newPos;
        }
    }

    public void MoveOn()
    {
        canScrollNow = true;
    }
    public void MoveOff()
    {
        canScrollNow = false;
    }
    bool checkCircleMover()
    {
        if (GetComponent<CircleMoveEnemy>() == null)
            return false;
        return true;
    }
}
