using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{

    public void Build(float height)
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

[System.Serializable]
public class LigntningConfig
{
    public float StartCellSize = 0.5f;
    public float StartEndSize = 0.5f;

    public int MinBranchInterval = 15;
    public int MaxBranchInterval = 30;

    public int MinBranchLength = 30;
    public int MaxBranchLength = 50;

    [Range(0f, 1f)] public float BranchVerticalChance = .1f;
    [Range(0f, 1f)] public float BranchDeviationChance = 0.5f;

}