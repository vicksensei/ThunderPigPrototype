using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    float dragspeed = 0.5f;
    Vector3 dragOrigin;
    bool cameraDragging = false;

    [SerializeField]
    Transform boundsTopRight;
    [SerializeField]
    Transform boundsBottomLeft;

    Vector3 origPos;
    Vector3 min, max;
    private void Start()
    {
        origPos = transform.position;
        min = new Vector3(boundsTopRight.position.x, boundsTopRight.position.y); 
        max = new Vector3(boundsBottomLeft.position.x, boundsBottomLeft.position.y); 
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !cameraDragging)
        {
            dragOrigin = Input.mousePosition;
            cameraDragging = true;
        }

        if (cameraDragging)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * -dragspeed,pos.y * -dragspeed);
            transform.Translate(move, Space.World);
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, min.x, max.x),
                Mathf.Clamp(transform.position.y, min.y, max.y),
                transform.position.z);
                
        }

        if (Input.GetMouseButtonUp(0))
        {
            cameraDragging = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            transform.position = origPos;
        }
    }
}
