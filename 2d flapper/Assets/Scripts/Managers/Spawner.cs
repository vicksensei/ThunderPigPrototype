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
        if (canSpawn.Value) //when spawning is on
        {
            // create a random number to select which spawnable to spawn
            int randomizer = Random.Range(0, spawnables.Length); 
            Spawnable current;
            current = spawnables[randomizer]; 
            
            //create values for the spawnable's position
            float y, x;

            //if we will randomize the height, create a random number between the top and bottom
            if (current.randomizeHeight)
            {
                y = Random.Range(
                    screenBottom.position.y + current.distanceFromBottom,
                    screenTop.position.y - current.distanceFromTop);
            }// otherwise,  place it a specific distance from the top.
            else
            {
                y = screenTop.position.y - current.distanceFromTop;
            }

            //if we will randomize the left right positon (rare)
            if (current.randomizeLR)
            {//create a random number between the  X position and Distance X
                x = Random.Range(transform.position.x -3, current.Distancex);
            }
            else
            {//otherwise, place it a specific distance from positionX
                x = transform.position.x + current.Distancex;
            }

            GameObject spawned;

            spawned = Instantiate(spawnables[randomizer].prefab,
                new Vector3(x, y, 0), 
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
    public bool randomizeHeight = true;
    public float Distancex = 0;
    public bool randomizeLR = false;
}
