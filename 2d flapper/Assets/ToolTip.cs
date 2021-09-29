using UnityEngine.EventSystems;
using UnityEngine;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string text;

    public GameObject template;
    public bool followsMouse;
    GameObject tipWindow;

    void OnMouseOver()
    {
    }

    private void OnMouseEnter()
    {
        Enter();
    }

    void OnMouseExit()
    {
        Exit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Enter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       Exit();
    }

    void Enter()
    {
        tipWindow = Instantiate(template);
        ToolTipController ttc = tipWindow.GetComponentInChildren<ToolTipController>();
        ttc.SetFollow(followsMouse);
        ttc.SetText(text);

    }
    void Exit()
    {
        Destroy(tipWindow);
    }
}
