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
    FloatValue spawnRate;
    [SerializeField]
    FloatValue delay;
    [SerializeField]
    BoolValue canSpawn;
    [SerializeField]
    BoolValue canScroll;

    [Header("State")]
    [SerializeField]
    GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        SpawningOff();
        InvokeRepeating("Spawn", delay.Value, spawnRate.Value);
        
    }


    void Spawn()
    {
        if (canSpawn.Value)
        {
            int randomizer = Random.Range(0, spawnables.Length);
            float y = Random.Range(
                screenBottom.position.y + spawnables[randomizer].distanceFromBottom, 
                screenTop.position.y - spawnables[randomizer].distanceFromTop);
            
            GameObject spawned;

            spawned = Instantiate(spawnables[randomizer].prefab,
                new Vector3(transform.position.x, y, 0), 
                Quaternion.identity, 
                transform);

        }
    }

    public void SpawningOn()
    {
        canSpawn.Value = true;
    }
    public void SpawningOff()
    {
        canSpawn.Value = false;
    }
    public void ScrollOn()
    {
        canScroll.Value = true;
    }
    public void ScrollOff()
    {
        canScroll.Value = false;
    }
}



[System.Serializable]
public class Spawnable
{
    public GameObject prefab;
    public float distanceFromBottom =0;
    public float distanceFromTop =0;
}
