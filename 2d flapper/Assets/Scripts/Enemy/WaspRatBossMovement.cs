using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspRatBossMovement : MonoBehaviour
{

    [Header("Combat")]
    [SerializeField]
    FloatValue Difficulty;


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
    GameObject ProjectilePrefab;
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
        minions *= (int)Difficulty.Value;
        speed += Difficulty.Value * .1f;
        StartPos = transform.position;
        transform.position = EnterPoint.position;
        currentPhase = CombatPhase.Appear;
    }

    void FixedUpdate()
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
            Move();
        }
    }
    void Angry()
    {
        if (currentPhase == CombatPhase.Enraged)
        {
            if (minionCount < minions )
            {
                Invoke("SpawnMinions", minionCount);
                minionCount++;
            }
            Move();
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
            Move();
        }
    }

    // 
    // Movements and behaviors
    //
    void SpawnMinions()
    {
        Instantiate(minionPrefab, transform);
    }

    bool isCharging;
    bool isShaking;
    bool isReseting;
    Vector3 ChargeTarget;
    Vector3 PreChargePos;
    void Move()
    {
        float x = StartPos.x;
        float y = StartPos.y + Mathf.PingPong(speed * Time.time, height);
        Vector3 pos = new Vector3(x, y);

        if (isShaking)
        {
            x = StartPos.x + Mathf.PingPong(10 * Time.time, .4f);
            pos = new Vector3(x, y);
        }
        else if (isCharging)
        {

            float step = speed * 10 * Time.deltaTime;
            pos = Vector2.MoveTowards(transform.position, ChargeTarget, step);

            //if (transform.position == ChargeTarget) stopCharging();
        }
        else if (isReseting)
        {
            float step = speed * 10 * Time.deltaTime;
            pos = Vector2.MoveTowards(transform.position, PreChargePos, step);
            if (transform.position == PreChargePos)
               stopReset();
        }
        transform.parent.position = pos;
    }


    void Charge()
    {
        waspBossAnimator.Play("WaspBossCharge");
        superArmorOn.Raise();
        PreChargePos = transform.position;
        ChargeTarget = new Vector3(transform.position.x - 20, transform.position.y);
    }

    void Shoot()
    {
        Instantiate(ProjectilePrefab,transform);
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
        CancelInvoke();
        currentPhase = CombatPhase.Bloodied;
        auraAnimator.Play("AuraOn");
        InvokeRepeating("Shoot", 1f, 4f);

    }

    public void GetEnraged()
    {
        CancelInvoke();
        currentPhase = CombatPhase.Enraged;
        InvokeRepeating("Charge", 1f, 7f);
    }

    public void Die()
    {
        currentPhase = CombatPhase.Dead;
        GameObject.Destroy(transform.parent.gameObject);
    }
    //
    // Charge Section
    //
    public void startShaking()
    {
        isShaking = true;
    }
    public void stopShaking()
    {
        isShaking = false;
    }
    public void startCharging()
    {
        isCharging = true;
    }
    public void stopCharging()
    {
        superArmorOff.Raise();
        transform.parent.position= new Vector3(PreChargePos.x, PreChargePos.y +10);
        isCharging = false;
    }
    public void startReset()
    {
        isReseting = true;
    }
    public void stopReset()
    {
        isReseting = false;
    }

}




