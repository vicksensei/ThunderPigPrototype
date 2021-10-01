
using UnityEngine;
using UnityEngine.UI;

public class PointRotator : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public RectTransform me;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       // Reposition();
    }

    public void Reposition()
    {
        Invoke("calcPos", .1f);
    }

    private void calcPos()
    {
        Image myImage = GetComponent<Image>();
        Vector3 pointA = point1.position;
        Vector3 pointB = point2.position;

        float pointDistance = (pointA - pointB).magnitude; //used for height of line

        float angle = Mathf.Atan2(pointB.x - pointA.x, pointA.y - pointB.y);
        if (angle < 0.0) { angle += Mathf.PI * 2; }
        angle *= Mathf.Rad2Deg;

        me.rotation = Quaternion.Euler(0, 0, angle); //rotate around the mid point
        me.position = point1.position;
        me.sizeDelta = new Vector2(2, pointDistance);
        myImage.enabled = true;
    }
}
