using TbsFramework.Units;
using TbsFramework.Cells;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnitBall : Unit
{
    [SerializeField]
    Animator highlighter;
    [SerializeField]
    SpriteRenderer icon;
    [SerializeField]
    SpriteRenderer ball;
    [SerializeField]
    StateBubble stateBubble;
    [SerializeField]
    Slider HPBar;


    public Color leadingColor;
    public override void Initialize()
    {
        base.Initialize();
        HPBar.maxValue = TotalHitPoints;
        CheckHealth();
        transform.localPosition -= new Vector3(0, 0, 1);
        stateBubble.ShowUnTouched();
        if (PlayerNumber == 0)
        {
            icon.color = Color.white;
            ball.color = Color.blue;

        }
        else
        {
            icon.color = Color.black;
            ball.color = Color.red;
        }

    }
    public override void MarkAsAttacking(Unit target)
    {
        CheckHealth();
    }

    public override void MarkAsDefending(Unit aggressor)
    {
        CheckHealth();
    }

    public override void MarkAsDestroyed()
    {
        CheckHealth();
    }

    public override void MarkAsFinished()
    {
        CheckState();
        CheckHealth();
    }

    public override void MarkAsFriendly()
    {
        CheckState();
        highlighter.Play("UnitUnhighlighted");
        CheckHealth();
    }

    public override void MarkAsReachableEnemy()
    {
        CheckHealth();
        Cell.MarkAsAttackableEnemy();
    }

    public override void MarkAsSelected()
    {
        CheckState();
        highlighter.Play("UnitSelected");
    }

    public override void UnMark()
    {
        CheckState();
        highlighter.Play("UnitUnhighlighted");
        CheckHealth();
    }

    void CheckState()
    {
        if (MovementPoints > 0 && ActionPoints > 0)
        {
            stateBubble.ShowUnTouched();
        }
        else if (MovementPoints <= 0 && ActionPoints <= 0) // no points
        {
            highlighter.Play("UnitDone");
            stateBubble.ShowNothing();
        }
        else if (MovementPoints > 0 && ActionPoints <= 0) // still has Move
        {
            stateBubble.ShowHasMoveLeft();
        }
        else if (MovementPoints <= 0 && ActionPoints > 0)//Still has action
        {

            bool enemyInRange = false;
            List<Cell> all = transform.parent.GetComponent<CellGridRef>().CellGrid.Cells;
            List<Cell> neighbors = Cell.GetNeighbours(all);
            foreach (Cell n in neighbors)
            {
                if (n.CurrentUnit != null)
                {
                    if (n.CurrentUnit.PlayerNumber != PlayerNumber)
                    {
                        enemyInRange = true;
                        break;
                    }
                }
            }
            if (!enemyInRange)
            {
                stateBubble.ShowNothing();
                highlighter.Play("UnitDone");
            }
            else
            {
                stateBubble.ShowHasAttackLeft();
            }
        }
    }

    void CheckHealth()
    {
        HPBar.gameObject.SetActive(HitPoints != TotalHitPoints);
        HPBar.value = HitPoints;
    }
}
