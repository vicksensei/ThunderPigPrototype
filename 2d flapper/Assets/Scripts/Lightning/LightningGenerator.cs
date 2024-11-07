using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningGenerator : MonoBehaviour
{
    [SerializeField] float LightningHeight = 75f;
    [SerializeField] bool DEBUG_GenerateLigtning;
    [SerializeField] Transform DEBUG_LightningTarget;

    [SerializeField] GameObject LightningPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DEBUG_GenerateLigtning)
        {
            DEBUG_GenerateLigtning = false;
            BuildLightning(DEBUG_LightningTarget.position, LightningHeight);
        }

    }

    public void BuildLightning(Vector3 target, float height)
    {
        var lightningGO = Instantiate(LightningPrefab, target, Quaternion.identity);
        var lightningScript = lightningGO.GetComponent<Lightning>();

        //lightningScript.Build(height);
    }
}

//[System.Serializable]
//public class LigntningConfig
//{
//    public float StartCellSize = 0.5f;
//    public float StartEndSize = 0.5f;

//    public int MinBranchInterval = 15;
//    public int MaxBranchInterval = 30;

//    public int MinBranchLength = 30;
//    public int MaxBranchLength = 50;

//    [Range(0f, 1f)] public float BranchVerticalChance = .1f;
//    [Range(0f, 1f)] public float BranchDeviationChance = 0.5f;

//}