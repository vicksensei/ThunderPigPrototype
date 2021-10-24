using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPathController : MonoBehaviour
{
    [SerializeField]
    MapNodeList map;

    [SerializeField]
    LineRenderer lrAvailablePaths;
    LineRenderer LR;
    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponent<LineRenderer>();
        List<Vector3> allPaths = new List<Vector3>();
        List<Vector3> unlockedPaths = new List<Vector3>();
        bool canContinue = true;
        for (int i = 0; i < map.mapNodes.Count; i++)
        {
            allPaths.Add(map.mapNodes[i].transform.position);
            if (map.mapNodes[i].IsLocked)
            {
                canContinue = false;
            }
            if (canContinue)
            {
                unlockedPaths.Add(map.mapNodes[i].transform.position);
            }

        }
        LR.positionCount = allPaths.Count;
        LR.SetPositions(allPaths.ToArray());
        lrAvailablePaths.positionCount = unlockedPaths.Count;
        lrAvailablePaths.SetPositions(unlockedPaths.ToArray());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
