using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 
    // Touch control Vars
    // 
    Vector2 fingerDown;
    DateTime fingerDownTime;

    Vector2 fingerUp;
    DateTime fingerUpTime;

    float swipeThreshold = 40f;
    float timeThreshold = 0.3f;
    Vector3 direction;
    // 
    // Exposed Vars
    // 
    [Header("Values")]
    public float SPEED;
    [SerializeField]
    ProgressionObject saveFile;

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


        if (gameState.state == GameState.State.Playing || gameState.state == GameState.State.BossFighting)
        {
            //
            // Keyboard controls
            // 
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                GravityOn();
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

            //
            // Touch controls
            // 


            if (Input.touchCount == 1)
                {
                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        fingerDown = Input.touches[0].position;
                        fingerUp = Input.touches[0].position;
                        fingerDownTime = DateTime.Now;
                    }
                    if (Input.touches[0].phase == TouchPhase.Ended)
                    {
                        fingerUp = Input.touches[0].position;
                        fingerUpTime = DateTime.Now;
                        if (IsSwipe())
                        {
                            CheckSwipeDirection();
                        }
                        else
                        {
                            Flap();
                        }

                    }

                    if (Input.touchCount == 2)
                    {
                        if (Input.touches[1].phase == TouchPhase.Ended)
                        {
                            Fire();
                        }

                    }
            }


            //
            // Mouse controls
            // 

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("click");
                    fingerDown = Input.mousePosition;
                    fingerUp = Input.mousePosition;
                fingerDownTime = DateTime.Now;
            }

            if (Input.GetMouseButtonUp(0))
                {
                    fingerUp = Input.mousePosition;
                    fingerUpTime = DateTime.Now;
                    if (IsSwipe())
                    {
                        CheckSwipeDirection();
                    }
                    else
                    {
                        Flap();
                    }

                }

            }
            if (Input.GetMouseButtonUp(1))
            {
                Fire();
            }
        }

    bool IsSwipe()
    {
        float duration = (float)fingerUpTime.Subtract(fingerDownTime).TotalSeconds;
        Vector2 directionVector = fingerUp - fingerDown;
        if (duration > timeThreshold)
        {
            return false;
        }
        if (directionVector.magnitude < swipeThreshold)
        {
            return false;
        }
        return true;
    }

    void CheckSwipeDirection()
    {
        Vector2 directionVector = fingerUp - fingerDown;
        if (fingerUp.x > fingerDown.x)
        {
            Right();
        }
        else
        {
            Left();
        }
    }



    void Fire()
    {
        if (saveFile.CurCharge > 0)
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
