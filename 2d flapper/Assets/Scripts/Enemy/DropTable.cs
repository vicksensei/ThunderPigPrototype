using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    [SerializeField]
    DropItem[] Drops;

    [SerializeField]
    IntValue numDrops;
    private void Awake()
    {
        Setup();
    }
    void Setup()
    {
        float lowBound = 0, upBound = 0;
        for (int i = 0; i < Drops.Length; i++)
        {
            if (i == 0)
            {
                lowBound = 0;
            }
            else
            {
                lowBound += Drops[i - 1].chance;
            }
            upBound = lowBound + Drops[i].chance;

            Drops[i].lbound = lowBound;
            Drops[i].ubound = upBound;
        }
    }
    void DropAnItem()
    {
        float randomizer = Random.Range(0f, 1f);
        for (int i = 0; i < Drops.Length; i++)
        {
            if(randomizer >= Drops[i].lbound && randomizer < Drops[i].ubound)
            {
                Instantiate(Drops[i].prefab, transform.position,Quaternion.identity);
            }
        }
    }

    public void BeforeDestroy()
    {
        for (int i = 0; i < numDrops.Value; i++)
        {
            DropAnItem();
        }
    }

}
[System.Serializable]
public class DropItem
{
    public GameObject prefab;
    public float chance;
    public float lbound,ubound;
}
    