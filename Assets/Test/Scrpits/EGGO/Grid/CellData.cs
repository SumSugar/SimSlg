using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TiledType
{
    forest,
    stream,
    plain
}

[System.Serializable]
public class CellData {
    public TiledType tiledType;
}
