using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    Spawnable[] spawnables;
    [Header("Coords")]
    [SerializeField]
    Transform screenTop;
    [SerializeField]
    Transform screenBottom;

    [Header("Rates")]
    [SerializeField]
    public float spawnRate;
    [SerializeField]
    public float delay;
    [Header("State")]
    [SerializeField]
    GameState gameState;
    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("Spawn", delay, spawnRate);
        
    }


    void Spawn()
    {
        if (gameState.state == GameState.State.Playing)
        {
            int randomizer = Random.Range(0, spawnables.Length);
            float y = Random.Range(
                screenBottom.position.y + spawnables[randomizer].distanceFromBottom, 
                screenTop.position.y - spawnables[randomizer].distanceFromTop);
            Debug.Log(y);

            GameObject spawned;

            //EnemyUpDown move;

            spawned = Instantiate(spawnables[randomizer].prefab,
                new Vector3(transform.position.x, y, 0), 
                Quaternion.identity, 
                transform);

        }
    }
}



[System.Serializable]
public class Spawnable
{
    public GameObject prefab;
    public float distanceFromBottom =0;
    public float distanceFromTop =0;
}
