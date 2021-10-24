using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTool : MonoBehaviour
{

    [SerializeField]
    GameObject prefab;
    [SerializeField]
    float radius;
    [SerializeField]
    int numberElements;



    List<GameObject> menuItems;
    int count=0;
    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
       // count++;
       // Rotate(count);
    }

    List<Vector3> GetOrbitCoordinates(Vector3 circleCenter, int NumberOfPoints, float radius, int rotation = 0)
    {
        if (NumberOfPoints == 0) { return null; }
        List<Vector3> coords = new List<Vector3>();

        for (int i = 1; i <= NumberOfPoints; i++)
        {
            float angleRadian = ((Mathf.PI * radius) / NumberOfPoints) * i;
            float x = Mathf.Cos(angleRadian  - rotation) * radius;
            float y = Mathf.Sin(angleRadian  - rotation) * radius;
            coords.Add(new Vector3(x, y) + circleCenter);
        }

        return coords;
    }

    void Rotate(int angle)
    {

        List<Vector3> pos = GetOrbitCoordinates(transform.position, numberElements, radius, angle);
        for (int i = 0; i < pos.Count; i++)
        {
            menuItems[i].transform.position = pos[i];
        }
    }

    void Generate()
    {

        menuItems = new List<GameObject>();
        List<Vector3> pos = GetOrbitCoordinates(transform.position, numberElements, radius);
        for (int i = 0; i < pos.Count; i++)
        {
            GameObject GO = Instantiate(prefab, pos[i], Quaternion.identity, transform);
            GO.GetComponentInChildren<Text>().text = (i + 1).ToString();
            menuItems.Add(GO);
        }

    }

    public void OnGenerate()
    {

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Generate();
    }

}
