using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    int iLevel;
    [SerializeField]
    int iOrder;
    [Header("References")]

    [SerializeField]
    SOEvents.IntEvent clickEvent;
    [SerializeField]
    Transform tOutline;
    [SerializeField]
    SpriteRenderer srPoint;
    [SerializeField]
    Transform tShadow;

    [Header("Colors")]
    [SerializeField]
    Color cNormal;
    [SerializeField]
    Color cCompleted;
    [SerializeField]
    Color cLocked;
    [SerializeField]
    Color cHover;
    [SerializeField]
    Color cActive;

    [Header("Progression")]
    [SerializeField]
    ProgressionObject PO;

    [SerializeField]
    bool isCompleted = false;
    [SerializeField]
    bool isLocked = false;
    Vector3 OriginalPos;

    public bool IsLocked { get => isLocked; }

    private void Start()
    {
        OriginalPos = tOutline.localPosition;
        if (isCompleted)
        {
            srPoint.color = cCompleted;
        }
        if (isLocked)
        {
            srPoint.color = cLocked;
        }
    }
    private void OnMouseEnter()
    {
        if(!isLocked)
        srPoint.color = cHover;
    }
    private void OnMouseExit()
    {
        if (!isLocked)
        {
            if (isCompleted)
            {
                srPoint.color = cCompleted;
            }
            else
            {
                srPoint.color = cNormal;
            }
        }

        tOutline.localPosition = OriginalPos;
    }

    private void OnMouseDown()
    {
        if (!isLocked)
        {
            tOutline.localPosition = tShadow.localPosition;
            srPoint.color = cActive;
        }
    }
    private void OnMouseUp()
    {
        tOutline.localPosition = OriginalPos;
        if (!isLocked)
        {
                srPoint.color = cHover;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (!isLocked)
        {
            srPoint.color = cHover;
            clickEvent.Raise(iOrder);
        }
    }

    void Lock()
    {
        tOutline.localPosition = OriginalPos;
        isLocked = true;
        srPoint.color = cLocked;
    }
    void UnLock()
    {
        tOutline.localPosition = OriginalPos;
        isLocked = false;
        if (isCompleted)
        {
            srPoint.color = cCompleted;
        }
        else
        {
            srPoint.color = cNormal;
        }
    }

    public void MarkAsCompleted()
    {

    }

}
