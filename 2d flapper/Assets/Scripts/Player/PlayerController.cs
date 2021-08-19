using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Values")]
    public float SPEED;
    [SerializeField]
    IntValue currentCharge;

    [Header("Prefabs")]
    public GameObject projectile;

    [Header("Events")]
    [SerializeField]
    SOEvents.VoidEvent PausePressed;
    [SerializeField]
    SOEvents.VoidEvent FlapEvent;
    [SerializeField]
    SOEvents.VoidEvent FireEvent;

    [Header("State")]
    [SerializeField]
    GameState gameState;

    Animator wing;
    Rigidbody2D rb;

    public bool isImmune = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        wing = gameObject.GetComponentInChildren<Animator>();
        GravityOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.state == GameState.State.Playing || gameState.state == GameState.State.BossFighting )
        {
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                Flap();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                Fire();
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                Left();
            }
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                Right();
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                PausePressed.Raise();
            }
        }
    }

    void Fire()
    {
        if (currentCharge.Value > 0)
        {
            FireEvent.Raise();
            Instantiate(projectile, transform.position, Quaternion.identity);
        }/* else play no charge sound*/
    }
    void Flap()
    {
        rb.velocity = Vector2.up * SPEED;
        wing.Play("TPFlapping");
        FlapEvent.Raise();
    }

    void Left()
    {
        rb.velocity = Vector2.left * SPEED;
    }
    void Right()
    {
        rb.velocity = Vector2.right * SPEED;
    }
    public void GravityOff()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
    }

    public void GravityOn()
    {
        rb.gravityScale = .6f;
    }
    public void OnPlayerCol()
    {
        rb.AddForce(Vector2.up*4);
    }
    
}
