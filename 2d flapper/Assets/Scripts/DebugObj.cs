using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObj : MonoBehaviour
{
    public void LogBulletCollided()
    {
        Debug.Log("BulletCollision!");
    }

    public void LogCollided()
    {
        Debug.Log("Collision!");
    }

    public void LogPlayerCollided()
    {
        Debug.Log("PlayerCollision!");
    }

}
