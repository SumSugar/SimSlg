using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
public class BuildManager :  Singleton<BuildManager> 
{
    public Room selectRoom;
    private Grid grid;
    public CellData cellData;
    public LayerMask mask;
    private bool isSecondSelect = false;
    private Cell select_first;
    private Cell select_last;
    // Use this for initialization
    protected override void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    protected virtual void Start()
    {
        GameUI.instance.stateChanged += OnGameUIStateChanged;
    }

    /// <summary>
    /// 选择房间区域范围
    /// EGGO
    /// </summary>
    public void SelecetRoomRange(UIPointer pointer)
    {
        Cell cell = grid.GetNearestPointOnGridCell(pointer.HitPosition);
        Debug.LogError(cell.index);
        if (isSecondSelect)
        {
            select_last = cell;
            isSecondSelect = !isSecondSelect;
            ComputeBlockCells(select_first, select_last);
            Debug.LogError("selectPoint_last");
        }
        else
        {
            select_first = cell;
            isSecondSelect = !isSecondSelect;
            Debug.LogError("selectPoint_first");
        }
    }

    public void CreatTiled(Cell _cell, GameObject _tiled)
    {
        _cell.InitCell(_tiled);
    }


    /// <summary>
    /// 计算两次点击范围内的Cell
    /// EGGO
    /// </summary>
    public Cell[] ComputeBlockCells(Cell _first, Cell _last)
    {
        int h = _last.x - _first.x;
        int v = _last.z - _first.z;
        int directionDelt_x = 0;
        int directionDelt_z = 0;
        if(h >= 0)
        {
            directionDelt_x = 0;
        }
        else
        {
            directionDelt_x = 4;
        }
        if (v >= 0)
        {
            directionDelt_z = 0;
        }
        else
        {
            directionDelt_z = 4;
        }


        h = System.Math.Abs(h) + 1;
        v = System.Math.Abs(v) + 1;
        Cell[] cells = new Cell[h * v];
        
        Cell headCell = _first;

        for (int i = 0; i < v; i++)
        {
            Cell tmpCell = headCell;
            for (int j = 0; j < h; j++)
            {
                //Debug.LogError(tmpCell.index);
                tmpCell.onTiled.SetActive(false);
                //...
                //...区域模块
                tmpCell = tmpCell.GetNeighbor((CellDirection)2 + directionDelt_x);
            }
            headCell = headCell.GetNeighbor((CellDirection)0 + directionDelt_z);
        }
        // Debug.LogError(System.Math.Abs(h * v));
        return cells;
    }

    /// <summary>
    /// 创建房间
    /// EGGO
    /// </summary>
    public Room CreateRoom()
    {
        Room room = new Room();
        return room;
    }

    /// <summary>
    /// GameUI状态改变委托
    /// EGGO
    /// </summary>
    protected void OnGameUIStateChanged(GameUI.State oldState, GameUI.State newState)
    {
        if (newState == GameUI.State.Normal)
        {
            isSecondSelect = false;
            //Hide();
        }
    }
}
