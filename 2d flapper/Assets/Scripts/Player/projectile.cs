using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    

    private void Update()
    {
        transform.position +=  Vector3.right * speed * Time.deltaTime;
    }


}
