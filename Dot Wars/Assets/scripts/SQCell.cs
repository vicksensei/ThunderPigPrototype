using TbsFramework.Cells;
using UnityEngine;

public class SQCell : Square
{
    [SerializeField]
    SpriteRenderer highlighter;
    [SerializeField]
    SpriteRenderer selector;

    public bool containsEnemy = false;

    public override Vector3 GetCellDimensions()
    {
        return GetComponent<Renderer>().bounds.size;
    }

    public override void MarkAsHighlighted()
    {
        selector.enabled = true;
    }

    public override void MarkAsPath()
    {
        highlighter.enabled = true;
        highlighter.color = new Color(0, 1f, 0, .8f);
    }

    public override void MarkAsReachable()
    {
        highlighter.enabled = true;
        highlighter.color = new Color(Color.cyan.r,Color.cyan.g,Color.cyan.b,.7f);
    }

    public override void UnMark()
    {
        selector.enabled = false;
        highlighter.enabled = false;
        containsEnemy = false;
    }

    public override void MarkAsAttackableEnemy()
    {
        containsEnemy = true;
        highlighter.enabled = true;
        highlighter.color = new Color(1f, 0, 0, .7f);
    }
}
