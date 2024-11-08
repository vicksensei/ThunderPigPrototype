using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField]
    Transform[] Layers;

    private void Update()
    {
        for (int i = 0; i < Layers.Length; i++)
        {
            Layers[i].transform.position += new Vector3((i + 1) * .25f * Time.deltaTime * -1, 0);
        }
    }

}
