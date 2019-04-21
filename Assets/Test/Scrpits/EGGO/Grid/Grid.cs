using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using System;

public class Grid : MonoBehaviour {
    GridMesh girdMesh;
    [SerializeField]
    private float cellSize = 2.0f;
    public int width = 20;
    Vector3 gridSize;
    Vector3 offPos;
    //public GameObject[] initCube;
    //public GameObject mapCube;
    //public GameObject[,] mapCubeArr;
    public Cell cellPrefab;
    public Cell[] cells;
    public GameObject[] tiledPrefabs;
    private void Awake()
    {
        girdMesh = GetComponent<GridMesh>();
        gridSize.x = width * cellSize;
        gridSize.y= width * cellSize;
        offPos = new Vector3(cellSize * 0.5f, 0, cellSize * 0.5f);
        CreatMap();
    }

    private void Start()
    {

    }


    private void CreatMap()
    {
        cells = new Cell[width* width];
        for (int z = 0 , i = 0; z < width; z++)
        {
            for (int x = 0; x < width; x++)
            {
                //Vector3 point = GetNearestPointOnGrid(new Vector3(x, 0F, z));
                ////Instantiate(initCube[Random.Range(0, initCube.Length)], point, gameObject.transform.rotation);
                //GameObject cube = Instantiate(mapCube, point, gameObject.transform.rotation);
                //cube.transform.SetParent(this.transform);
                //mapCubeArr[(int)x, (int)z] = cube;
                //GameObject prefeb =  Instantiate(initCube[Random.Range(0, initCube.Length)], cube.transform.position, gameObject.transform.rotation);
                //prefeb.transform.SetParent(cube.transform);
                //cube.GetComponent<MapCube>().TiledObject = prefeb;
                Cell cell =  CreatCell(x, z , i++);
                //cell.InitCell(tiledPrefabs[Random.Range(0, tiledPrefabs.Length)]);
                cell.InitCell(tiledPrefabs[2]);
                //Debug.LogError(mapCubeArr[(int)x, (int)z]);
            }
        }
    }

    private Cell CreatCell(int _x, int _z , int _i)
    {

        Vector3 position;
        position.x = _x * cellSize;
        position.y = 0f;
        position.z = _z * cellSize;
        Cell cell = cells[_i] = Instantiate<Cell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.x = _x;
        cell.z = _z;
        cell.index = _x + _z * width;
        if (_x > 0)
        {
            cell.SetNeighbor(CellDirection.W , cells[_i - 1]);
        }
        if (_z > 0)
        {
            cell.SetNeighbor(CellDirection.S, cells[_i - width]);
        }
        if (_z > 0 && _x < width - 1)
        {
            cell.SetNeighbor(CellDirection.SE, cells[_i - width + 1]);
        }
        if (_x > 0 && _z > 0)
        {
            cell.SetNeighbor(CellDirection.SW, cells[_i - width - 1]);
        }
        return cell;
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position , bool _isOffset)
    {
        if (_isOffset)
        {
            position -= transform.position - offPos;//坐标系由网格中心偏移之左下角
        }
        int xCount = Mathf.RoundToInt(position.x / cellSize);
        int yCount = Mathf.RoundToInt(position.y / cellSize);
        int zCount = Mathf.RoundToInt(position.z / cellSize);

        Vector3 result = new Vector3(
            (float)xCount * cellSize,
            (float)yCount * cellSize,
            (float)zCount * cellSize
            );
        result += transform.position;
        return result;
    }

    public Vector3 GetFootPoint(Vector3 position , IntVector2 size)
    {
        Vector3 result = new Vector3(position.x - size.x*cellSize * 0.5f, position.y , position.z - size.y * cellSize * 0.5f);
        return result;
    }

    public Vector3 SetCenterPoint(Vector3 position, IntVector2 size)
    {
        Vector3 result = new Vector3(position.x + size.x * cellSize * 0.5f , position.y, position.z + size.y * cellSize * 0.5f) - offPos;
        return result;
    }

    //public IntVector2 GetNearestPointOnGrid2D(Vector3 position)
    //{
    //    position -= transform.position;
    //    int xCount = Mathf.RoundToInt(position.x / cellSize);
    //    int yCount = Mathf.RoundToInt(position.y / cellSize);
    //    int zCount = Mathf.RoundToInt(position.z / cellSize);

    //    IntVector2 result = new IntVector2(
    //        xCount * (int)cellSize,
    //        zCount * (int)cellSize
    //        );
    //    IntVector2 trans_pos = new IntVector2(
    //      (int)transform.position.x,
    //      (int)transform.position.z
    //      );
    //    result += trans_pos;
    //    return result;
    //}



    public Cell GetNearestPointOnGridCell(Vector3 position)
    {
        Vector3 point = GetNearestPointOnGrid(position , false);
        int index = (int)(point.x / cellSize + (point.z / cellSize) * width);
        return cells[index];
    }

    /// <summary>
    /// Tests whether the indicated cell range represents a valid placement location.
    /// </summary>
    /// <param name="gridPos">The grid location</param>
    /// <param name="size">The size of the item</param>
    /// <returns>Whether the indicated range is valid for placement.</returns>
    public RoomFitStatus Fits(Vector3 rayPos, IntVector2 size)
    {
        ///暂替
        Vector3 foot_pos = GetFootPoint(rayPos, size);
        foot_pos = GetNearestPointOnGrid(foot_pos, true);
        IntVector2 gridPos = new IntVector2((int)foot_pos.x / 2, (int)foot_pos.z / 2);
        ///

        // If the tile size of the tower exceeds the dimensions of the placement area, immediately decline placement.
        if ((size.x > gridSize.x) || (size.y > gridSize.y))
        {
            return RoomFitStatus.OutOfBounds;
        }

        IntVector2 extents = gridPos + size;

        // Out of range of our bounds
        if ((gridPos.x < 0) || (gridPos.y < 0) ||
            (extents.x > gridSize.x) || (extents.y > gridSize.y))
        {
            return RoomFitStatus.OutOfBounds;
        }

        // Ensure there are no existing towers within our tile silhuette.
        for (int y = gridPos.y; y < extents.y; y++)
        {
            for (int x = gridPos.x; x < extents.x; x++)
            {
                int index = x + y * width;
                if (cells[index].isFil)
                {
                    return RoomFitStatus.Overlaps;
                }
            }
        }

        // If we've got this far, we've got a valid position.
        return RoomFitStatus.Fits;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float z = 0; z < width * cellSize; z += cellSize)
        {
            for (float x = 0; x < width * cellSize; x += cellSize)
            {
                Vector3 point = GetNearestPointOnGrid(new Vector3(x, 0F, z) , false);
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }

    public void WorldToGird()
    {

    }
    /// <summary>
    /// Sets a cell range as being occupied by a tower.
    /// </summary>
    /// <param name="gridPos">The grid location</param>
    /// <param name="size">The size of the item</param>
    public void Occupy(IntVector2 gridPos, IntVector2 size)
    {
        IntVector2 extents = gridPos + size;

        // Validate the dimensions and size
        if ((size.x > gridSize.x) || (size.y > gridSize.y))
        {
             throw new ArgumentOutOfRangeException("size", "Given dimensions do not fit in our grid");
        }

        // Out of range of our bounds
        if ((gridPos.x < 0) || (gridPos.y < 0) ||
            (extents.x > gridSize.x) || (extents.y > gridSize.y))
        {
            throw new ArgumentOutOfRangeException("gridPos", "Given footprint is out of range of our grid");
        }

        // Fill those positions
        for (int y = gridPos.y; y < extents.y; y++)
        {
            for (int x = gridPos.x; x < extents.x; x++)
            {
                int index = x + y * width;
                cells[index].currentTile.SetState(PlacementTileState.Filled);
                cells[index].isFil = true;
                Debug.LogError("Change" + index);
            }
        }
    }
}
