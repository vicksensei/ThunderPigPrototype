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
    [SerializeField]
    Animator waspBossAnimator;
    [SerializeField]
    Animator auraAnimator;

    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent superArmorOn;
    [SerializeField]
    SOEvents.VoidEvent superArmorOff;

    int minionCount;
    bool isCharging;
    bool isShaking;

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
    //
    // Phase Behavior functions
    //
    void Enter()
    {
        if (currentPhase == CombatPhase.Appear)
        {


            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, StartPos, step);

            if(transform.position == StartPos)
            {
                currentPhase = CombatPhase.Healthy;
                superArmorOff.Raise();
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
            MoveUpDown();
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
            if (minionCount < minions * 3)
            {
                Invoke("SpawnMinions", minionCount * .7f);
                minionCount++;
            }
            MoveUpDown();
        }
    }

    // 
    // Movements and behaviors
    //
    void SpawnMinions()
    {
        Instantiate(minionPrefab, transform);
    }

    void MoveUpDown()
    {
        float x = StartPos.x;
        float y = StartPos.y + Mathf.PingPong(speed * Time.time, height);
        if (isShaking)
        {
            x = (StartPos.x - 2f) + Mathf.PingPong(10 * Time.time, .4f);
            y = StartPos.y;
        }
        else if (isCharging)
        {
            // Make charging motions
        }
        Vector3 pos = new Vector3(x, y);
        transform.parent.position = pos;
    }


    void Charge()
    {
        waspBossAnimator.Play("WaspBossCharge");
    }




    //
    // Event Functions
    //
    public void minionDied()
    {
        minionCount--;
    }

    public void GetBloodied()
    {
        currentPhase = CombatPhase.Bloodied;
        auraAnimator.Play("AuraOn");
        Debug.Log("GetBloodied!");

    }

    public void GetEnraged()
    {
        currentPhase = CombatPhase.Enraged;
        InvokeRepeating("Charge", 1f, 5f);
        Debug.Log("GetEnraged!");
    }

    public void Die()
    {
        currentPhase = CombatPhase.Dead;
        GameObject.Destroy(gameObject);
    }

    public void startShaking()
    { isShaking = true; }
    public void stopShaking()
    { isShaking = false; }
}



