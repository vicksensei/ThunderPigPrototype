using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightningLines : MonoBehaviour
{
    LineRenderer LR;
    [SerializeField]
    Transform lineOrigin;
    [SerializeField]
    Transform lineEnd;
    [SerializeField]
    int vertexes;
    [SerializeField]
    float magnitude;

    List<Vector3> points;
    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponent<LineRenderer>();
        LRDraw();
    }

    public void LRDraw()
    {
        LR.positionCount = vertexes +1;
        points = new List<Vector3>();
        points.Add(lineOrigin.position);
        Vector3 last = lineOrigin.position;
        for (int i = 1; i < vertexes; i++)
        {
            Vector3 current = new Vector3(last.x + Random.Range(-magnitude, magnitude), last.y + Random.Range(-magnitude, magnitude));
            while (Vector3.Distance(last, lineEnd.position) < Vector3.Distance(current, lineEnd.position))
            {
                current = new Vector3(last.x + Random.Range(-magnitude, magnitude), last.y + Random.Range(-magnitude, magnitude));
            }
            points.Add(current);
            last = current;

        }
        points.Add(lineEnd.position);
        LR.SetPositions(points.ToArray());
    }

}
