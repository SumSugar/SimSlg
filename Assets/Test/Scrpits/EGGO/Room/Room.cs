using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    public IntVector2 m_size;
    protected int m_level;
    Grid m_grid;
    GameObject placementArea;
    IntVector2 gridPosition;
    /// <summary>
    /// Gets the first level tower ghost prefab
    /// </summary>
    public RoomPlacementGhost roomGhostPrefab;
    protected Cell[] blockCells;

    public void Init(Cell[] cell)
    {
        blockCells = cell;
    }

    /// <summary>
    /// Provide the tower with data to initialize with
    /// </summary>
    /// <param name="targetArea">The placement area configuration</param>
    /// <param name="destination">The destination position</param>
    public virtual void Initialize(GameObject targetArea, Vector3 destination)
    {
        ///测试用
        m_grid = FindObjectOfType<Grid>();
        ///

        placementArea = targetArea;
        //gridPosition = destination;

        if (targetArea != null)
        {
            Vector3 foot_pos = m_grid.GetFootPoint(destination, m_size);  //计算左下角坐标
            foot_pos = m_grid.GetNearestPointOnGrid(foot_pos , true);     //修正左下角左边
            Vector3 center_pos = m_grid.SetCenterPoint(foot_pos, m_size); // 根据左下角坐标修正中心坐标
            transform.position = center_pos;
            transform.rotation = placementArea.transform.rotation;
            m_grid.Occupy(new IntVector2 ((int)foot_pos.x /2 , (int)foot_pos.z /2 ) , m_size); //...缺少转换网格坐标函数，暂替，
            //IntVector2 result = new IntVector2(destination.x);
        }

        //SetLevel(0);
        //if (LevelManager.instanceExists)
        //{
        //    LevelManager.instance.levelStateChanged += OnLevelStateChanged;
        //}
    }
}
