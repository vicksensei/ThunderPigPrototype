
using UnityEngine;
using TMPro;

public class ToolTipController : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;

    RectTransform me;
    bool followsMouse = false;

    public void SetText(string newText)
    {
        text.text = newText;
        //Immitate();
        Invoke("Immitate",.02f);
    }

    void Immitate()
    {
        GetComponent<SizeMatcher>().Immitate();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void SetFollow(bool follow)
    {
        FollowMouse();
        followsMouse = follow;
    }

    private void Update()
    {
        if (followsMouse)
        {
            FollowMouse();
        }
    }

    void FollowMouse()
    {
        me = GetComponent<RectTransform>();
        Vector3 MP = Input.mousePosition;
        int xmult = 1;
        int ymult = 1;
        int offset = 20;
        Vector2 Tsize = me.sizeDelta;
        if (MP.y > Tsize.y + Screen.height / 2)
        {
            ymult = -1;
        }
        if (MP.x > Tsize.x + Screen.width / 2)
        {
            xmult = -1;
        }
        transform.position = new Vector3(MP.x + xmult * (Tsize.x / 2 + offset), MP.y + ymult * (Tsize.y / 2 + offset));

    }
}
