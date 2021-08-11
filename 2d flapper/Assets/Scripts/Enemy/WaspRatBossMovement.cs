using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspRatBossMovement : MonoBehaviour
{

    [Header("Combat")]
    [SerializeField]
    IntValue HP;
    [SerializeField]
    IntValue MaxHP;


    [Header("Location")]
    [SerializeField]
    Transform EnterPoint;
    Vector3 StartPos;


    [Header("Movement")]
    [SerializeField]
    float speed;
    [SerializeField]
    float height;

    [SerializeField]
    GameObject minionPrefab;
    [SerializeField]
    int minions = 3;

    [Header("State")]
    [SerializeField]
    GameState gameState;

    int minionCount;

    public enum CombatPhase
    {
        Appear,
        Healthy,
        Bloodied,
        Enraged,
        Dead

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
        if (gameState.state == GameState.State.BossFighting)
        {
            Enter();
            Normal();
            Angry();
            Hurt();
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
                Invoke("SpawnMinions", minionCount*1.33f);
                minionCount++;
            }


            float y = StartPos.y + (Mathf.PingPong(speed * Time.time, height));
            Vector3 pos = new Vector3(transform.position.x, y, transform.position.z);
            transform.position = pos;
        }
    }
    void Angry()
    {
        if (currentPhase == CombatPhase.Enraged)
        {
        }
    }
    void Hurt()
    {
        if (currentPhase == CombatPhase.Bloodied)
        {
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

    public void GetBloodied()
    {
        currentPhase = CombatPhase.Bloodied;
    }

    public void GetEnraged()
    {
        currentPhase = CombatPhase.Enraged;
    }

    public void Die()
    {
        currentPhase = CombatPhase.Dead;
        GameObject.Destroy(gameObject);
    }
}



