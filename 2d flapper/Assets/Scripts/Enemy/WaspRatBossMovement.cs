using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspRatBossMovement : MonoBehaviour
{
    [SerializeField]
    IntValue HP;

    [SerializeField]
    Transform EnterPoint;
    Vector3 StartPos;


    [SerializeField]
    float speed;

    [SerializeField]
    GameObject minionPrefab;
    [SerializeField]
    int minions;

    [Header("State")]
    [SerializeField]
    GameState gameState;

    int minionCount;

    public enum CombatPhase
    {
        Appear,
        Healthy,
        Bloodied,
        Enraged

    }

    CombatPhase currentPhase;

    void Awake()
    {
        StartPos = transform.position;
        transform.position = EnterPoint.position;
        currentPhase = CombatPhase.Appear;
    }

    void Update()
    {
        if (gameState.state == GameState.State.Playing)
        {
            Enter();
            Normal();
        }
    }

    void Enter()
    {
        if (currentPhase == CombatPhase.Appear)
        {


            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, StartPos, step);

            if(transform.position == StartPos)
            {
                currentPhase = CombatPhase.Healthy;
            }

        }
    }

    void Normal()
    {
        if (currentPhase == CombatPhase.Healthy)
        {
            if (minionCount < minions)
            {
                Invoke("SpawnMinions", minionCount*2);
                minionCount++;
            }


            float y = StartPos.y + (Mathf.PingPong(speed * Time.time, Height) * multiplier);
            Vector3 pos = new Vector3(transform.position.x, y, transform.position.z);

        }
    }

    void SpawnMinions()
    {
        Instantiate(minionPrefab, transform);
    }


    public void minionDied()
    {
        minionCount--;
    }



}



