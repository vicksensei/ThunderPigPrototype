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
    BoolValue canScrollNow;

    [SerializeField]
    FloatValue Difficulty;

    [SerializeField]
    bool isEnemy = true;

    float speed;
    // Update is called once per frame
    private void Awake()
    {
        speed = ScrollSpeed.Value;
        if (isEnemy)
        {
            speed += Difficulty.Value * .1f;
        }   
    }
    void FixedUpdate()
    {
        if (canScrollNow.Value)
        {
            Vector3 newPos = new Vector3(1 * speed * Time.deltaTime, 0, 0);
            if (checkCircleMover())
            {
                GetComponent<CircleMoveEnemy>().Center -= newPos;
            }
            else
                transform.position -= newPos;
        }
    }

    bool checkCircleMover()
    {
        if (GetComponent<CircleMoveEnemy>() == null)
            return false;
        return true;
    }
}
