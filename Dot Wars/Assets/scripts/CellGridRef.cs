using TbsFramework.Grid;
using UnityEngine;

public class CellGridRef : MonoBehaviour
{
    [SerializeField]
    CellGrid cellGrid;

    public CellGrid CellGrid { get => cellGrid;  }
}
