using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cell : MonoBehaviour {
    public int x;
    public int z;
    public int index;
    public PlacementTile tilePrefab;
    public PlacementTile currentTile;
    float size = 1f;
    public Cell[] neibighbors;
    public GameObject onTiled = null;
    public bool isFil;
    public Cell GetNeighbor(CellDirection _direction)
    {
        return neibighbors[(int)_direction];
    }

    public void SetNeighbor(CellDirection _direction , Cell _cell)
    {
        neibighbors[(int)_direction] = _cell;
        _cell.neibighbors[(int)_direction.Opposite()] = this;
    }

    public void InitCell(GameObject _tiled)
    {
        GameObject obj= Instantiate<GameObject>(_tiled);
        currentTile =  Instantiate<PlacementTile>(tilePrefab);
        currentTile.SetState(PlacementTileState.Empty);
        currentTile.transform.SetParent(GameObject.Find("TileGroup").transform, false);
        currentTile.transform.position = transform.position;
        obj.transform.SetParent(transform, false);
        onTiled = obj;
        isFil = false;
    }
}
