using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField]
    GameObject upDownEnemy;
    [SerializeField]
    GameObject downUpEnemy;
    [SerializeField]
    GameObject circleEnemy;

    [Header("Coords")]
    [SerializeField]
    Transform screenTop;
    [SerializeField]
    Transform screenBottom;


    [Header("Values")]
    [SerializeField]
    float rate;
    [SerializeField]
    bool enemy;


    [Header("State")]
    [SerializeField]
    GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 2.0f, rate);
    }

    void Spawn()
    {
        if (gameState.state == GameState.State.Playing)
        {
            int randomizer = Random.Range(0, 2);
            GameObject spawned;
            EnemyUpDown move;
            switch (randomizer)
            {
                case 1:
                    spawned = Instantiate(upDownEnemy);
                    if (enemy)
                    {
                        move = spawned.GetComponent<EnemyUpDown>();
                        move.StartPos = new Vector3(transform.position.x, Random.Range(screenBottom.position.y - move.Height, screenTop.position.y));
                        spawned.transform.position = move.StartPos;
                    }
                    else
                    {
                        spawned.transform.position = new Vector3(transform.position.x, Random.Range(screenBottom.position.y, screenTop.position.y));
                    }
                    break;
                case 0:
                    spawned = Instantiate(downUpEnemy);
                    if (enemy)
                    {
                        move = spawned.GetComponent<EnemyUpDown>();
                        move.StartPos = new Vector3(transform.position.x, Random.Range(screenBottom.position.y, screenTop.position.y + move.Height));
                        spawned.transform.position = move.StartPos;
                    }
                    else
                    {
                        spawned.transform.position = new Vector3(transform.position.x, Random.Range(screenBottom.position.y, screenTop.position.y));
                    }
                    break;
            }
        }
    }
}
