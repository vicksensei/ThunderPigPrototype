using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBubble : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer icon;
    [SerializeField]
    Sprite CanMove;
    [SerializeField]
    Sprite CanAttack;
    [SerializeField]
    Sprite CanBoth;

    public void ShowUnTouched()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        sr.color = Color.yellow;
        icon.enabled = true;
        icon.sprite = CanBoth;
    }
    public void ShowHasMoveLeft()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        sr.color = Color.white;
        icon.enabled = true;
        icon.sprite = CanMove;
    }
    public void ShowHasAttackLeft()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        sr.color = Color.white;
        icon.enabled = true;
        icon.sprite = CanAttack;
    }
    public void ShowNothing()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        icon.enabled = false;
    }
}
